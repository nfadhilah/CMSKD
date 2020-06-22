using Application.Dtos;
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

namespace Application.TUBEND.TBPLDetKegCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdTBPLDet { get; set; }
      public int? IdNoJeTra { get; set; }
      public long? IdKeg { get; set; }
      public decimal? Nilai { get; set; }
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
        var parameters = new List<Expression<Func<TBPLDetKeg, bool>>>();

        if (request.IdTBPLDet.HasValue)
          parameters.Add(d => d.IdTBPLDet == request.IdTBPLDet);

        if (request.IdNoJeTra.HasValue)
          parameters.Add(d => d.IdNoJeTra == request.IdNoJeTra);

        if (request.IdKeg.HasValue)
          parameters.Add(d => d.IdKeg == request.IdKeg);

        if (request.Nilai.HasValue)
          parameters.Add(d => d.Nilai == request.Nilai);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.TBPLDetKeg.FindAll(predicate).Count();

        var result = await _context.TBPLDetKeg
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdTBPLDetKeg)
          .FindAllAsync<TBPLDet, JTrnlKas, MKegiatan>(
            predicate, x => x.TBPLDet,
            x => x.JTrnlKas, x => x.Kegiatan);

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