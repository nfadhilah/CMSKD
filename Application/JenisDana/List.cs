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

namespace Application.JenisDana
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public string KdDana { get; set; }
      public string NmDana { get; set; }
      public string Ket { get; set; }
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
        var parameters = new List<Expression<Func<JDana, bool>>>();

        if (!string.IsNullOrWhiteSpace(request.KdDana))
          parameters.Add(d => d.KdDana.Contains(request.KdDana));

        if (!string.IsNullOrWhiteSpace(request.NmDana))
          parameters.Add(d => d.NmDana.Contains(request.NmDana));

        if (!string.IsNullOrWhiteSpace(request.Ket))
          parameters.Add(d => d.Ket.Contains(request.Ket));

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.JDana.FindAll(predicate).Count();

        var result = await _context.JDana
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdJDana)
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