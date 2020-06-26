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

namespace Application.TUBEND.BKPajakCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdUnit { get; set; }
      public long? IdBend { get; set; }
      public string NoBkPajak { get; set; }
      public int? IdxKode { get; set; }
      public string KdStatus { get; set; }
      public DateTime? TglBkPajak { get; set; }
      public string Uraian { get; set; }
      public DateTime? TglValid { get; set; }
      public long? KdRilis { get; set; }
      public int? StKirim { get; set; }
      public int? StCair { get; set; }
      public int? IdTransfer { get; set; }
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
        var parameters = new List<Expression<Func<BkPajak, bool>>>();

        if (request.IdUnit.HasValue)
          parameters.Add(d => d.IdUnit == request.IdUnit);

        if (!string.IsNullOrWhiteSpace(request.NoBkPajak))
          parameters.Add(d => d.NoBkPajak.Contains(request.NoBkPajak));

        if (!string.IsNullOrWhiteSpace(request.Uraian))
          parameters.Add(d => d.Uraian == request.Uraian);

        if (!string.IsNullOrWhiteSpace(request.KdStatus))
          parameters.Add(d => d.KdStatus == request.KdStatus);

        if (request.TglBkPajak.HasValue)
          parameters.Add(d => d.TglBkPajak == request.TglBkPajak);

        if (request.StKirim.HasValue)
          parameters.Add(d => d.StKirim == request.StKirim);

        if (request.StCair.HasValue)
          parameters.Add(d => d.StCair == request.StCair);

        if (request.IdTransfer.HasValue)
          parameters.Add(d => d.IdTransfer == request.IdTransfer);

        if (request.IdBend.HasValue)
          parameters.Add(d => d.IdBend == request.IdBend);

        if (request.IdxKode.HasValue)
          parameters.Add(d => d.IdxKode == request.IdxKode);

        if (request.KdRilis.HasValue)
          parameters.Add(d => d.KdRilis == request.KdRilis);

        if (request.TglValid.HasValue)
          parameters.Add(d => d.TglValid == request.TglValid);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.BkPajak.FindAll(predicate).Count();

        var result = await _context.BkPajak
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.NoBkPajak)
          .FindAllAsync<DaftUnit, Bend>(
            predicate, x => x.Unit, x => x.Bend);

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