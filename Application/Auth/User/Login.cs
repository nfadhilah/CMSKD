using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using FluentValidation;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Auth.User
{
  public class Login
  {
    public class Command : IRequest<Profile>
    {
      public string UserName { get; set; }
      public string Password { get; set; }
      public long AppId { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(x => x.UserName).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.AppId).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, Profile>
    {
      private readonly IDbContext _context;
      private readonly IJwtGenerator _jwtGenerator;
      private readonly IMapper _mapper;
      private readonly IPasswordHasher _passwordHasher;

      public Handler(
        IDbContext context, IJwtGenerator jwtGenerator, IMapper mapper,
        IPasswordHasher passwordHasher)
      {
        _context = context;
        _jwtGenerator = jwtGenerator;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
      }

      public async Task<Profile> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var user =
          await _context.WebUser.GetUserWithRelation(request.UserName,
            request.AppId);

        if (user == null)
          throw new ApiException(
            "Login gagal. Harap cek username dan password anda.",
            (int) HttpStatusCode.Unauthorized);

        if (user.BlokId > 3)
          throw new ApiException(
            "User diblokir. Hubungi admin untuk membuka blokir.");

        if (!user.IsAuthorized.HasValue || !user.IsAuthorized.Value)
          throw new ApiException("User anda belum di otorisasi",
            (int) HttpStatusCode.Unauthorized);

        if (!_passwordHasher.Validate(user.Pwd, request.Password))
        {
          user.BlokId = ++user.BlokId;

          _context.WebUser.Update(user);

          throw new ApiException(
            "Login gagal. Harap cek username dan password anda.",
            (int) HttpStatusCode.Unauthorized);
        }

        var token = _jwtGenerator.CreateToken(user, request.AppId);

        return new Profile
        {
          UserName = user.UserId.Trim(),
          DisplayName = user.Nama.Trim(),
          Token = token,
          Image = null,
          KdTahap = user.KdTahap,
          IdPeg = user.IdPeg,
          UnitId = user.IdUnit?.ToString() ?? "",
          KdUnit = user.DaftUnit?.KdUnit.Trim() ?? "",
          NmUnit = user.DaftUnit?.NmUnit.Trim() ?? ""
        };
      }
    }
  }
}