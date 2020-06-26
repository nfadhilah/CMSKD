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

namespace Application.TUBEND.TBPLCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdUnit { get; set; }
      public string NoTBPL { get; set; }
      public long? IdBend { get; set; }
      public string KdStatus { get; set; }
      public int? IdxKode { get; set; }
      public DateTime? TglTBPL { get; set; }
      public string Penyetor { get; set; }
      public string Alamat { get; set; }
      public string UraiTBPL { get; set; }
      public DateTime? TglValid { get; set; }
      public long? KdRilis { get; set; }
      public int? StKirim { get; set; }
      public int? StCair { get; set; }
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
        var parameters = new List<Expression<Func<TBPL, bool>>>();

        if (request.IdUnit.HasValue)
          parameters.Add(d => d.IdUnit == request.IdUnit);

        if (!string.IsNullOrWhiteSpace(request.NoTBPL))
          parameters.Add(d => d.NoTBPL.Contains(request.NoTBPL));

        if (!string.IsNullOrWhiteSpace(request.KdStatus))
          parameters.Add(d => d.KdStatus == request.KdStatus);

        if (!string.IsNullOrWhiteSpace(request.Penyetor))
          parameters.Add(d => d.Penyetor.Contains(request.Penyetor));

        if (request.TglTBPL.HasValue)
          parameters.Add(d => d.TglTBPL == request.TglTBPL);

        if (request.TglValid.HasValue)
          parameters.Add(d => d.TglValid == request.TglValid);

        if (request.IdBend.HasValue)
          parameters.Add(d => d.IdBend == request.IdBend);

        if (request.KdRilis.HasValue)
          parameters.Add(d => d.KdRilis == request.KdRilis);

        if (request.IdxKode.HasValue)
          parameters.Add(d => d.IdxKode == request.IdxKode);

        if (request.StKirim.HasValue)
          parameters.Add(d => d.StKirim == request.StKirim);

        if (request.StCair.HasValue)
          parameters.Add(d => d.StCair == request.StCair);

        if (request.TglValid.HasValue)
          parameters.Add(d => d.TglValid == request.TglValid);

        if (!string.IsNullOrWhiteSpace(request.Alamat))
          parameters.Add(d => d.Alamat.Contains(request.Alamat));

        if (!string.IsNullOrWhiteSpace(request.UraiTBPL))
          parameters.Add(d => d.UraiTBPL.Contains(request.UraiTBPL));

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.TBPL.FindAll(predicate).Count();

        var result = await _context.TBPL
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.NoTBPL)
          .FindAllAsync<DaftUnit, StatTrs, Bend, ZKode>(
            predicate, x => x.Unit,
            x => x.StatTrs, x => x.Bend, x => x.ZKode);

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