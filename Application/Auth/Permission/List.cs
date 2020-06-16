using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Helpers;
using AutoMapper;
using Domain.DM;
using MediatR;
using Persistence;

namespace Application.Auth.Permission
{
  public class List
  {
    public class Query : IRequest<IEnumerable<PermissionDto>>
    {
      public int? RoleId { get; set; }
      public int? MenuId { get; set; }
    }

    public class
      Handler : IRequestHandler<Query, IEnumerable<PermissionDto>>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<IEnumerable<PermissionDto>> Handle(
        Query request, CancellationToken cancellationToken)
      {
        var parameters = new List<Expression<Func<Domain.Auth.Permission, bool>>>();

        if (request.RoleId.HasValue)
          parameters.Add(p => p.RoleId == request.RoleId);

        if (request.MenuId.HasValue)
          parameters.Add(p => p.MenuId == request.MenuId);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var result =
          await _context.Permission
            .FindAllAsync<Menu, Roles>(predicate,
              p => p.Menu, p => p.Role);

        return _mapper.Map<IEnumerable<PermissionDto>>(result);
      }
    }
  }
}