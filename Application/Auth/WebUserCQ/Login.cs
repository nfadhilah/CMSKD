using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.Auth;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Auth.WebUserCQ
{
  public class Login
  {
    public class Command : IRequest<Profile>
    {
      public string UserName { get; set; }
      public string Password { get; set; }
      public int AppId { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(x => x.UserName).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, Profile>
    {
      private readonly IDbContext _context;
      private readonly IJwtGenerator _jwtGenerator;
      private readonly IMapper _mapper;
      private readonly IPasswordHasher _passwordHasher;

      public Handler(IDbContext context, IJwtGenerator jwtGenerator,
        IMapper mapper, IPasswordHasher passwordHasher)
      {
        _context = context;
        _jwtGenerator = jwtGenerator;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
      }

      public async Task<Profile> Handle(Command request, CancellationToken cancellationToken)
      {
        var user = (await _context.WebUser.FindAllAsync<WebGroup, DaftUnit>(x => x.UserId == request.UserName,
            x => x.WebGroup, x => x.DaftUnit))
          .FirstOrDefault();

        if (user == null)
          throw new ApiException("Login gagal. Harap cek username dan password anda.",
            (int) HttpStatusCode.Unauthorized);

        if (user.BlokId.HasValue && user.BlokId > 3)
          throw new ApiException(
            "User diblokir. Hubungi admin untuk membuka blokir.");

        if (!_passwordHasher.Validate(user.Pwd.Trim(), request.Password))
        {
          user.BlokId = ++user.BlokId;

          await _context.WebUser.UpdateAsync(user);

          throw new ApiException("Login gagal. Harap cek username/password anda.",
            (int) HttpStatusCode.Unauthorized);
        }

        var token = _jwtGenerator.CreateToken(user, request.AppId);

        return new Profile
        {
          UserName = user.UserId.Trim(),
          DisplayName = user.Nama.Trim(),
          Token = token,
          Image = user.Photo,
          KdTahap = user.KdTahap,
          UnitKey = user.UnitKey ?? "",
          KdUnit = user.DaftUnit?.KdUnit ?? "",
          NmUnit = user.DaftUnit?.NmUnit ?? ""
        };
      }
    }
  }
}