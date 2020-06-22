using Application.Dtos;
using Application.Helpers;
using Domain.DM;
using Domain.MA;
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

namespace Application.TUBEND.SPPCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdUnit { get; set; }
      public string NoSPP { get; set; }
      public string KdStatus { get; set; }
      public int? KdBulan { get; set; }
      public long? IdBend { get; set; }
      public long? IdSPD { get; set; }
      public long? IdPhk3 { get; set; }
      public int? IdxKode { get; set; }
      public string NoReg { get; set; }
      public string KetOtor { get; set; }
      public string NoKontrak { get; set; }
      public string Keperluan { get; set; }
      public string Penolakan { get; set; }
      public DateTime? TglValid { get; set; }
      public DateTime? TglSPP { get; set; }
      public DateTime? Status { get; set; }
      public DateTime? DateCreate { get; set; }
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
        var parameters = new List<Expression<Func<SPP, bool>>>();

        if (request.IdUnit.HasValue)
          parameters.Add(d => d.IdUnit == request.IdUnit);

        if (!string.IsNullOrWhiteSpace(request.NoKontrak))
          parameters.Add(d => d.NoKontrak.Contains(request.NoKontrak));

        if (!string.IsNullOrWhiteSpace(request.NoSPP))
          parameters.Add(d => d.NoSPP.Contains(request.NoSPP));

        if (!string.IsNullOrWhiteSpace(request.KdStatus))
          parameters.Add(d => d.KdStatus == request.KdStatus);

        if (request.IdPhk3.HasValue)
          parameters.Add(d => d.IdPhk3 == request.IdPhk3);

        if (request.KdBulan.HasValue)
          parameters.Add(d => d.KdBulan == request.KdBulan);

        if (request.IdBend.HasValue)
          parameters.Add(d => d.IdBend == request.IdBend);

        if (request.IdSPD.HasValue)
          parameters.Add(d => d.IdSPD == request.IdSPD);

        if (request.IdxKode.HasValue)
          parameters.Add(d => d.IdxKode == request.IdxKode);

        if (request.TglSPP.HasValue)
          parameters.Add(d => d.TglSPP == request.TglSPP);

        if (request.TglValid.HasValue)
          parameters.Add(d => d.TglValid == request.TglValid);

        if (!string.IsNullOrWhiteSpace(request.NoReg))
          parameters.Add(d => d.NoReg.Contains(request.NoReg));

        if (!string.IsNullOrWhiteSpace(request.KetOtor))
          parameters.Add(d => d.KetOtor.Contains(request.KetOtor));

        if (!string.IsNullOrWhiteSpace(request.Keperluan))
          parameters.Add(d => d.Keperluan.Contains(request.Keperluan));

        if (!string.IsNullOrWhiteSpace(request.Penolakan))
          parameters.Add(d => d.Penolakan.Contains(request.Penolakan));

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.SPP.FindAll(predicate).Count();

        var result = await _context.SPP
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.NoSPP)
          .FindAllAsync<DaftUnit, StatTrs, Bend, SPD, DaftPhk3, ZKode>(
            predicate, x => x.Unit,
            x => x.StatTrs, x => x.Bendahara, x => x.SPD, x => x.Phk3,
            x => x.ZKode);

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