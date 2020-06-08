using Application.Dtos;
using Application.Helpers;
using Domain;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.JenisBendahara
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public string Jns_Bend { get; set; }
      public long? IdRek { get; set; }
      public string Urai_Bend { get; set; }
    }

    public class Handler : IRequestHandler<Query, PaginationWrapper>
    {
      private readonly IDbContext _context;

      public Handler(IDbContext context)
      {
        _context = context;
      }

      public async Task<PaginationWrapper> Handle(
        Query request, CancellationToken cancellationToken)
      {
        var parameters = new List<Expression<Func<JBend, bool>>>();

        if (!string.IsNullOrWhiteSpace(request.Jns_Bend))
          parameters.Add(d => d.Jns_Bend.Contains(request.Jns_Bend));

        if (request.IdRek.HasValue)
          parameters.Add(d => d.IdRek == request.IdRek.Value);

        if (!string.IsNullOrWhiteSpace(request.Urai_Bend))
          parameters.Add(d => d.Urai_Bend.Contains(request.Urai_Bend));

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.JBend.FindAll(predicate).Count();

        var result = await _context.JBend
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdJBend)
          .FindAllAsync(predicate);

        return new PaginationWrapper(result, new Pagination
        {
          CurrentPage = request.CurrentPage,
          PageSize = request.PageSize,
          TotalItemsCount = totalItemsCount
        });
      }
    }
  }
}