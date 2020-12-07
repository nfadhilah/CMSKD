using Application.Auth.WebUserCQ;
using Application.Common.DTOS;
using Application.Interfaces;
using AutoMapper;
using AutoWrapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Security;
using Infrastructure.Utilities;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Persistence;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Globalization;
using System.Text;


namespace API
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddTransient<IDbContext>(provider =>
      {
        var context = provider.GetService<IHttpContextAccessor>().HttpContext;

        var year = context?.Request.Headers["x-api-year"];

        return int.TryParse(year, out var dbYear)
          ? new DbContext(Configuration.SetDbYear(dbYear))
          : new DbContext(Configuration.SetDbYear());
      });

      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(opt =>
        {
          var key =
            new SymmetricSecurityKey(
              Encoding.UTF8.GetBytes(Configuration["TokenKey"]));

          opt.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
          };
        });

      services.AddAuthorization(opt =>
      {
        opt.AddPolicy("ManagerPaketRUP",
          policy => policy.RequireRole("PPK, ADMINRUP"));
      });

      services.AddSwaggerGen(opt =>
      {
        opt.SwaggerDoc("v1",
          new OpenApiInfo { Title = "SIPKD API", Version = "v1" });

        opt.CustomSchemaIds(x => x.FullName);

        opt.AddFluentValidationRules();

        opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
          In = ParameterLocation.Header,
          Description = "Please insert JWT with Bearer into field",
          Name = "Authorization",
          Type = SecuritySchemeType.ApiKey
        });

        opt.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
          {
            new OpenApiSecurityScheme
            {
              Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              }
            },
            new string[] { }
          }
        });

        // var apiXmlFile =
        //   $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        // var appXmlFile =
        //   $"{typeof(MappingProfile).Assembly.GetName().Name}.xml";
        // var apiXmlPath = Path.Combine(AppContext.BaseDirectory, apiXmlFile);
        // var appXmlPath = Path.Combine(AppContext.BaseDirectory, appXmlFile);
        // opt.IncludeXmlComments(apiXmlPath);
        // opt.IncludeXmlComments(appXmlPath);
      });

      services.AddCors(opt =>
      {
        opt.AddPolicy("CorsPolicy", policy =>
        {
          policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()
            .SetIsOriginAllowed(x => _ = true)
            .WithExposedHeaders("WWW-Authenticate");
          // .WithOrigins("http://localhost:3000")
          // .AllowCredentials();
        });
      });

      services.AddControllers(opt =>
        {
          var policy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser().Build();

          opt.Filters.Add(new AuthorizeFilter(policy));
        })
        .AddFluentValidation(opt =>
          {
            opt.RegisterValidatorsFromAssemblyContaining<Login>();
            opt.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            ValidatorOptions.LanguageManager.Culture = new CultureInfo("id-ID");
          }
        ).AddNewtonsoftJson(opt =>
        {
          opt.SerializerSettings.ReferenceLoopHandling =
            ReferenceLoopHandling.Ignore;

          opt.SerializerSettings.Formatting = Formatting.Indented;

          opt.SerializerSettings.ContractResolver =
            new CamelCasePropertyNamesContractResolver();
        });

      services.AddMediatR(typeof(Login.Handler).Assembly);

      services.AddAutoMapper(typeof(Login.Handler).Assembly);

      services.AddScoped<IJwtGenerator, JwtGenerator>();

      services.AddScoped<IUserAccessor, UserAccessor>();

      services.AddSingleton(typeof(ISqlGenerator<>), typeof(SqlGenerator<>));

      services.AddSingleton<IPasswordHasher, PasswordHasher>();

      services.AddSingleton<IEncryptionHelper, EncryptionHelper>();

      services.Configure<FileSettings>(Configuration.GetSection(FileSettings.Section));

      // services.AddSingleton<IBendDocNumGenerator, BendDocNumGenerator>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      // app.UseHttpsRedirection();

      Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzYxNjk4QDMxMzgyZTMzMmUzMGd2dnZ6bkhiandVakJVLzVPOGxVUkJud2huR3FybUYwQUJIWG43L0NVN1E9");

      app.UseDefaultFiles();

      app.UseStaticFiles();

      app.UseSwagger();

      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("../swagger/v1/swagger.json", "SIPKD API V1");
      });

      app.UseRouting();

      app.UseCors("CorsPolicy");

      app.UseAuthentication();

      app.UseAuthorization();

      app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions
      {
        IsDebug = env.IsDevelopment(),
        IsApiOnly = false,
      });

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
        // endpoints.MapFallbackToController("Index", "Fallback");
      });
    }
  }
}