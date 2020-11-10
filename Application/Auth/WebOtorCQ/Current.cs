using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using Dapper;
using Domain.Auth;
using MediatR;
using Persistence;

namespace Application.Auth.WebOtorCQ
{
  public class Current
  {
    public class Query : IRequest<IEnumerable<WebOtorDTO>>
    {
    }

    public class Handler : IRequestHandler<Query, IEnumerable<WebOtorDTO>>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;
      private readonly IUserAccessor _userAccessor;

      public Handler(IDbContext context, IMapper mapper, IUserAccessor userAccessor)
      {
        _context = context;
        _mapper = mapper;
        _userAccessor = userAccessor;
      }

      public async Task<IEnumerable<WebOtorDTO>> Handle(Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.WebOtor.FindAllAsync<WebRole, WebGroup>(
            x => x.GroupId == _userAccessor.GetCurrentRoleId() && x.WebRole.AppId == _userAccessor.GetCurrentAppId(),
            x => x.WebRole, x => x.WebGroup);

        return _mapper.Map<IEnumerable<WebOtorDTO>>(result);
      }
    }
  }
}