using Application.Helpers;
using Domain.DM;
using Domain.MA;
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

namespace Application.MA.SPDCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdUnit { get; set; }
      public string NoSPD { get; set; }
      public DateTime? TglSPD { get; set; }
      public int? KdBulan1 { get; set; }
      public int? KdBulan2 { get; set; }
      public int? IdxKode { get; set; }
      public string Keterangan { get; set; }
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
        var parameters = new List<Expression<Func<SPD, bool>>>();

        if (request.IdUnit.HasValue)
          parameters.Add(d => d.IdUnit == request.IdUnit);

        if (!string.IsNullOrWhiteSpace(request.NoSPD))
          parameters.Add(d => d.NoSPD.Contains(request.NoSPD));

        if (request.TglSPD.HasValue)
          parameters.Add(d => d.TglSPD == request.TglSPD);

        if (request.KdBulan1.HasValue)
          parameters.Add(d => d.KdBulan1 == request.KdBulan1);

        if (request.TglValid.HasValue)
          parameters.Add(d => d.TglValid == request.TglValid);

        if (request.KdBulan2.HasValue)
          parameters.Add(d => d.KdBulan2 == request.KdBulan2);

        if (request.IdxKode.HasValue)
          parameters.Add(d => d.IdxKode == request.IdxKode);

        if (!string.IsNullOrWhiteSpace(request.Keterangan))
          parameters.Add(d => d.Keterangan.Contains(request.Keterangan)); ;

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.SPD.FindAll(predicate).Count();

        var result = await _context.SPD
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.NoSPD)
          .FindAllAsync<DaftUnit>(predicate, x => x.Unit);

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