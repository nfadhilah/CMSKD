using Application.Helpers;
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
using Application.CommonDTO;

namespace Application.DM.BKBKasCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdUnit { get; set; }
      public long? IdRek { get; set; }
      public long? IdBank { get; set; }
      public string NmBKas { get; set; }
      public string NoRek { get; set; }
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
        var parameters = new List<Expression<Func<BkBKas, bool>>>();

        if (request.IdUnit.HasValue)
          parameters.Add(d => d.IdUnit == request.IdUnit);

        if (request.IdRek.HasValue)
          parameters.Add(d => d.IdRek == request.IdRek);

        if (request.IdBank.HasValue)
          parameters.Add(d => d.IdBank == request.IdBank);

        if (!string.IsNullOrWhiteSpace(request.NmBKas))
          parameters.Add(d => d.NmBKas == request.NmBKas);

        if (!string.IsNullOrWhiteSpace(request.NoRek))
          parameters.Add(d => d.NoRek == request.NoRek);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.BkBKas.FindAll(predicate).Count();

        var result = await _context.BkBKas
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.NoBBantu)
          .FindAllAsync<DaftUnit, DaftRekening>(predicate, c => c.DaftUnit, c => c.DaftRekening);

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