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

namespace Application.JenisBuktiKas
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public string KdBKas { get; set; }
      public string NmBKas { get; set; }
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
        var parameters = new List<Expression<Func<JBKas, bool>>>();

        if (!string.IsNullOrWhiteSpace(request.KdBKas))
          parameters.Add(d => d.KdBKas.Contains(request.KdBKas));

        if (!string.IsNullOrWhiteSpace(request.NmBKas))
          parameters.Add(d => d.NmBKas.Contains(request.NmBKas));

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.JBKas.FindAll(predicate).Count();

        var result = await _context.JBKas
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdBKas)
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