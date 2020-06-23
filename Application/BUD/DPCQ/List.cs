using Application.Dtos;
using Application.Helpers;
using Domain.BUD;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BUD.DPCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public string NoDP { get; set; }
      public int? IdxKode { get; set; }
      public long? IdTtd { get; set; }
      public DateTime? TglDP { get; set; }
      public string Uraian { get; set; }
      public DateTime? TglValid { get; set; }
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
        var parameters = new List<Expression<Func<DP, bool>>>();

        if (request.IdTtd.HasValue)
          parameters.Add(d => d.IdTtd == request.IdTtd);

        if (!string.IsNullOrWhiteSpace(request.Uraian))
          parameters.Add(d => d.Uraian.Contains(request.Uraian));

        if (!string.IsNullOrWhiteSpace(request.NoDP))
          parameters.Add(d => d.NoDP == request.NoDP);

        if (request.TglDP.HasValue)
          parameters.Add(d => d.TglDP == request.TglDP);

        if (request.TglValid.HasValue)
          parameters.Add(d => d.TglValid == request.TglValid);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.DP.FindAll(predicate).Count();

        var result = await _context.DP
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.NoDP)
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