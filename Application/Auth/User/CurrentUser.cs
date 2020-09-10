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
          await _context.WebUser.GetUserWithRelation(
            _userAccessor.GetCurrentUsername(),
            _userAccessor.GetCurrentAppId());

        if (user == null)
          throw new ApiException(
            "Login gagal. Harap cek username dan password anda.",
            (int) HttpStatusCode.Unauthorized);

        if (user.BlokId > 3)
          throw new ApiException(
            "User diblokir. Hubungi admin untuk membuka blokir.");

        var token =
          _jwtGenerator.CreateToken(user, _userAccessor.GetCurrentAppId());

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