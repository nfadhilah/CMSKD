using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using Domain.Auth;
using MediatR;
using Persistence;

namespace Application.Auth.WebGroupCQ
{
  public class List
  {
    public class Query : IRequest<IEnumerable<WebGroup>>
    {
      public List<string> ExcludedRoleName { get; set; }
    }

    public class Handler : IRequestHandler<Query, IEnumerable<WebGroup>>
    {
      private readonly IDbContext _context;
      private readonly IUserAccessor _userAccessor;

      public Handler(
        IDbContext context, IUserAccessor userAccessor)
      {
        _context = context;
        _userAccessor = userAccessor;
      }

      public async Task<IEnumerable<WebGroup>> Handle(
        Query request, CancellationToken cancellationToken)
      {
        return await _context.WebGroup.GetListWebGroup(
          _userAccessor.GetCurrentAppId(), request.ExcludedRoleName);
      }
    }
  }
}