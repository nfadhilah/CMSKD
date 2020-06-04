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
  public class CurrentUser
  {
    public class Query : IRequest<Profile> { }

    public class Validator : AbstractValidator<Query>
    {
      public Validator() { }
    }

    public class Handler : IRequestHandler<Query, Profile>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;
      private readonly IUserAccessor _userAccessor;
      private readonly IJwtGenerator _jwtGenerator;

      public Handler(
        IDbContext context, IMapper mapper, IUserAccessor userAccessor,
        IJwtGenerator jwtGenerator
      )
      {
        _context = context;
        _mapper = mapper;
        _userAccessor = userAccessor;
        _jwtGenerator = jwtGenerator;
      }

      public async Task<Profile> Handle(
        Query request, CancellationToken cancellationToken)
      {
        var user =
          (await _context.AppUser.FindAllAsync<DaftUnit, UserRoles>(
            x => x.UserName == _userAccessor.GetCurrentUsername(), c => c.DaftUnit,
            c => c.UserRoles)).FirstOrDefault();

        if (user == null)
          throw new ApiException("Login gagal. Harap cek username dan password anda.",
            (int)HttpStatusCode.Unauthorized);

        if (user.LockedOut)
          throw new ApiException(
            "User diblokir. Hubungi admin untuk membuka blokir.");

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
          UnitId = user.UnitId?.ToString().Trim() ?? "",
          KdUnit = user.DaftUnit?.KdUnit.Trim() ?? "",
          NmUnit = user.DaftUnit?.NmUnit.Trim() ?? ""
        };
      }
    }
  }
}