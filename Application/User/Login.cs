using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.User
{
  public class Login
  {
    public class Query : IRequest<Profile>
    {
      public string UserName { get; set; }
      public string Password { get; set; }
    }

    public class Validator : AbstractValidator<Query>
    {
      public Validator()
      {
        RuleFor(x => x.UserName).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Query, Profile>
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
        Query request, CancellationToken cancellationToken)
      {
        var user =
          (await _context.AppUser.FindAllAsync<DaftUnit, UserRoles>(
            x => x.UserName == request.UserName, c => c.DaftUnit,
            c => c.UserRoles)).FirstOrDefault();

        if (user == null)
          throw new ApiException("Bad Username/Password",
            (int)HttpStatusCode.Unauthorized);

        if (user.LockedOut)
          throw new ApiException(
            "User terblokir. Hubungi admin untuk membuka blokir.");

        if (!_passwordHasher.Validate(user.Password, request.Password))
        {
          user.FalseLoginCount = ++user.FalseLoginCount;

          if (user.FalseLoginCount >= 3) user.LockedOut = true;

          _context.AppUser.Update(user);

          throw new ApiException("Bad Username/Password",
            (int)HttpStatusCode.Unauthorized);
        }

        var roleIds = user.UserRoles.Select(s => s.RoleId);

        var roles =
          _context.Roles.FindAll(
            r => roleIds.Contains(r.Id));

        var token = _jwtGenerator.CreateToken(user, roles);

        return new Profile
        {
          DisplayName = user.DisplayName,
          Token = token,
          Image = null,
          UserName = user.UserName,
          KdTahap = user.KdTahap,
          UnitKey = user.UnitKey?.Trim() ?? "",
          KdUnit = user.DaftUnit?.KdUnit.Trim() ?? "",
          NmUnit = user.DaftUnit?.NmUnit.Trim() ?? ""
        };
      }
    }
  }
}