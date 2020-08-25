using Application.Interfaces;
using AutoMapper;
using Domain.Auth;
using FluentValidation;
using MediatR;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Auth.RoleMenu
{
  public class CurrentMenu
  {
    public class Query : IRequest<IEnumerable<WebOtorDto>> { }

    public class Validator : AbstractValidator<Query>
    {
      public Validator() { }
    }

    public class Handler : IRequestHandler<Query, IEnumerable<WebOtorDto>>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;
      private readonly IUserAccessor _userAccessor;

      public Handler(
        IDbContext context, IMapper mapper, IUserAccessor userAccessor,
        IJwtGenerator jwtGenerator
      )
      {
        _context = context;
        _mapper = mapper;
        _userAccessor = userAccessor;
      }

      public async Task<IEnumerable<WebOtorDto>> Handle(
        Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.WebOtor.FindAllAsync<WebRole, WebGroup>(
            x => x.GroupId == _userAccessor.GetCurrentRoleId() &&
                 x.WebRole.IdApp == _userAccessor.GetCurrentAppId(),
            x => x.WebRole,
            x => x.WebGroup);

        return _mapper.Map<IEnumerable<WebOtorDto>>(result);
      }
    }
  }
}