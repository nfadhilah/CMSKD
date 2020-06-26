using Application.Helpers;
using Domain.DM;
using Domain.TUBEND;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.CommonDTO;

namespace Application.TUBEND.SPPDetBDanaCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdSPPDetB { get; set; }
      public string KdDana { get; set; }
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
        var parameters = new List<Expression<Func<SPPDetBDana, bool>>>();

        if (request.IdSPPDetB.HasValue)
          parameters.Add(d => d.IdSPPDetB == request.IdSPPDetB);

        if (!string.IsNullOrWhiteSpace(request.KdDana))
          parameters.Add(d => d.KdDana == request.KdDana);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.SPPDetBDana.FindAll(predicate).Count();

        var result = await _context.SPPDetBDana
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdSPPDetBDana)
          .FindAllAsync<SPPDetR, JDana>(
            predicate, x => x.SPPDetB,
            x => x.JDana);

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