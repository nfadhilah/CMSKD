using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.User
{
  public class Login
  {
    public class Query : IRequest<User>
    {
      public string UserId { get; set; }
      public string Password { get; set; }
    }

    public class Validator : AbstractValidator<Query>
    {
      public Validator()
      {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Query, User>
    {
      private readonly IDbContext _context;
      private readonly IJwtGenerator _jwtGenerator;
      private readonly IMapper _mapper;

      public Handler(
        IDbContext context, IJwtGenerator jwtGenerator, IMapper mapper)
      {
        _context = context;
        _jwtGenerator = jwtGenerator;
        _mapper = mapper;
      }

      public async Task<User> Handle(
        Query request, CancellationToken cancellationToken)
      {
        var user =
          await _context.WebUser.FindAsync<WebGroup, DaftUnit>(
            w => w.UserId == request.UserId, w => w.WebGroup, w => w.DaftUnit);

        if (user == null)
          throw new ApiException("Bad Username/Password",
            (int)HttpStatusCode.Unauthorized);

        if (user.BlokId.HasValue && user.BlokId.Value > 3)
          throw new ApiException(
            "User terblokir. Hubungi admin untuk membuka blokir.");

        if (Encode(request.Password) != user.Pwd.Trim())
        {
          user.BlokId = ++user.BlokId;

          _context.WebUser.Update(user);

          throw new ApiException("Bad Username/Password",
            (int)HttpStatusCode.Unauthorized);
        }

        var token = _jwtGenerator.CreateToken(user);

        return new User
        {
          DisplayName = user.Nama.Trim(),
          Token = token,
          Image = null,
          UserName = user.UserId.Trim(),
          KdTahap = user.KdTahap,
          UnitKey = user.UnitKey?.Trim() ?? "",
          KdUnit = user.DaftUnit?.KdUnit.Trim() ?? "",
          NmUnit = user.DaftUnit?.NmUnit.Trim() ?? ""
        };
      }

      private static string Encode(string str)
      {
        var md5 = new MD5CryptoServiceProvider();
        var encoding = Encoding.GetEncoding("Windows-1252");
        var result = md5.ComputeHash(encoding.GetBytes(str));
        return encoding.GetString(result);
      }
    }
  }
}