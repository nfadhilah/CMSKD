using Application.Rekanan;
using AutoMapper;
using AutoWrapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Persistence;
using System.Globalization;
using Swashbuckle.AspNetCore.Swagger;

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
      services.AddSingleton<IDbContext, DbContext>(provider =>
        new DbContext(Configuration.GetConnectionString("Default")));

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1",
          new OpenApiInfo { Title = "SIPKD API", Version = "v1" });

        c.CustomSchemaIds(x => x.FullName);

        c.AddFluentValidationRules();
      });

      services.AddControllers()
        .AddFluentValidation(opt =>
          {
            opt.RegisterValidatorsFromAssemblyContaining<Create>();
            opt.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            ValidatorOptions.LanguageManager.Culture = new CultureInfo("id-ID");
          }
        );

      services.AddMediatR(typeof(List.Handler).Assembly);

      services.AddAutoMapper(typeof(List.Handler).Assembly);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      // app.UseHttpsRedirection();

      app.UseSwagger();

      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("../swagger/v1/swagger.json", "SIPKD API V1");
      });

      app.UseRouting();

      app.UseAuthorization();

      app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions
      {
        IsDebug = env.IsDevelopment()
      });

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}