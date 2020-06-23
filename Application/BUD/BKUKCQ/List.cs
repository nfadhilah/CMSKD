using Application.Dtos;
using Application.Helpers;
using Domain.BUD;
using Domain.DM;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BUD.BKUKCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdKas { get; set; }
      public long? IdSP2D { get; set; }
      public long? IdBKas { get; set; }
      public long? IdUnit { get; set; }
      public DateTime? TglKas { get; set; }
      public DateTime? TglValid { get; set; }
      public string Uraian { get; set; }
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
        var parameters = new List<Expression<Func<BKUK, bool>>>();

        if (request.IdUnit.HasValue)
          parameters.Add(d => d.IdUnit == request.IdUnit);

        if (!string.IsNullOrWhiteSpace(request.Uraian))
          parameters.Add(d => d.Uraian.Contains(request.Uraian));

        if (request.IdKas.HasValue)
          parameters.Add(d => d.IdKas == request.IdKas);

        if (request.IdSP2D.HasValue)
          parameters.Add(d => d.IdSP2D == request.IdSP2D);

        if (request.IdBKas.HasValue)
          parameters.Add(d => d.IdBKas == request.IdBKas);

        if (request.TglKas.HasValue)
          parameters.Add(d => d.TglKas == request.TglKas);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.BKUK.FindAll(predicate).Count();

        var result = await _context.BKUK
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdBKUK)
          .FindAllAsync<DaftUnit, SP2D>(
            predicate, x => x.Unit, x => x.SP2D);

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