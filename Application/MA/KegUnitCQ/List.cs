using Application.Dtos;
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

namespace Application.MA.KegUnitCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdUnit { get; set; }
      public long? IdKeg { get; set; }
      public string KdTahap { get; set; }
      public long? IdPrgrm { get; set; }
      public int? NoPrior { get; set; }
      public long? IdSifatKeg { get; set; }
      public long? IdPeg { get; set; }
      public DateTime? TglAkhir { get; set; }
      public DateTime? TglAwal { get; set; }
      public Decimal? TargetP { get; set; }
      public string Lokasi { get; set; }
      public Decimal? JumlahMin1 { get; set; }
      public Decimal? Pagu { get; set; }
      public Decimal? JumlahPls1 { get; set; }
      public string Sasaran { get; set; }
      public string KetKeg { get; set; }
      public string IdPrioDa { get; set; }
      public string IdSas { get; set; }
      public string Target { get; set; }
      public string TargetIf { get; set; }
      public string TargetSen { get; set; }
      public string Volume { get; set; }
      public string Volume1 { get; set; }
      public string Satuan { get; set; }
      public Decimal? PaguPlus { get; set; }
      public Decimal? PaguTif { get; set; }
      public DateTime? TglValid { get; set; }
      public DateTime? DateCreate { get; set; }
      public DateTime? DateUpdate { get; set; }
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
        var parameters = new List<Expression<Func<KegUnit, bool>>>();

        if (request.IdUnit.HasValue)
          parameters.Add(d => d.IdUnit == request.IdUnit);
        if (request.IdKeg.HasValue)
          parameters.Add(d => d.IdKeg == request.IdKeg);
        if (!string.IsNullOrWhiteSpace(request.KdTahap))
          parameters.Add(d => d.KdTahap == request.KdTahap);
        if (request.IdPrgrm.HasValue)
          parameters.Add(x => x.IdPrgrm == request.IdPrgrm);
        if (request.NoPrior.HasValue)
          parameters.Add(d => d.NoPrior == request.NoPrior);
        if (request.IdSifatKeg.HasValue)
          parameters.Add(d => d.IdSifatKeg == request.IdSifatKeg);
        if (request.IdPeg.HasValue)
          parameters.Add(d => d.IdPeg == request.IdPeg);
        if (request.TglAkhir.HasValue)
          parameters.Add(d => d.TglAkhir == request.TglAkhir);
        if (request.TglAwal.HasValue)
          parameters.Add(d => d.TglAwal == request.TglAwal);
        if (request.TargetP.HasValue)
          parameters.Add(d => d.TargetP == request.TargetP);
        if (!string.IsNullOrWhiteSpace(request.Lokasi))
          parameters.Add(d => d.Lokasi == request.Lokasi);
        if (request.JumlahMin1.HasValue)
          parameters.Add(d => d.JumlahMin1 == request.JumlahMin1);
        if (request.Pagu.HasValue)
          parameters.Add(d => d.Pagu == request.Pagu);
        if (request.JumlahPls1.HasValue)
          parameters.Add(d => d.JumlahPls1 == request.JumlahPls1);
        if (!string.IsNullOrWhiteSpace(request.Sasaran))
          parameters.Add(d => d.Sasaran == request.Sasaran);
        if (!string.IsNullOrWhiteSpace(request.KetKeg))
          parameters.Add(d => d.KetKeg == request.KetKeg);
        if (!string.IsNullOrWhiteSpace(request.IdPrioDa))
          parameters.Add(d => d.IdPrioDa == request.IdPrioDa);
        if (!string.IsNullOrWhiteSpace(request.IdSas))
          parameters.Add(d => d.IdSas == request.IdSas);
        if (!string.IsNullOrWhiteSpace(request.Target))
          parameters.Add(d => d.Target == request.Target);
        if (!string.IsNullOrWhiteSpace(request.TargetIf))
          parameters.Add(d => d.TargetIf == request.TargetIf);
        if (!string.IsNullOrWhiteSpace(request.TargetSen))
          parameters.Add(d => d.TargetSen == request.TargetSen);
        if (!string.IsNullOrWhiteSpace(request.Volume))
          parameters.Add(d => d.Volume == request.Volume);
        if (!string.IsNullOrWhiteSpace(request.Volume1))
          parameters.Add(d => d.Volume1 == request.Volume1);
        if (!string.IsNullOrWhiteSpace(request.Satuan))
          parameters.Add(d => d.Satuan == request.Satuan);
        if (request.PaguPlus.HasValue)
          parameters.Add(x => x.PaguPlus == request.PaguPlus);
        if (request.PaguTif.HasValue)
          parameters.Add(x => x.PaguTif == request.PaguTif);
        if (request.TglValid.HasValue)
          parameters.Add(d => d.TglValid == request.TglValid);
        if (request.DateCreate.HasValue)
          parameters.Add(d => d.DateCreate == request.DateCreate);
        if (request.DateUpdate.HasValue)
          parameters.Add(d => d.DateUpdate == request.DateUpdate);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.KegUnit.FindAll(predicate).Count();

        var result = await _context.KegUnit
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdKegUnit)
          .FindAllAsync<MPgrm, MKegiatan>(predicate, c => c.MPgrm, c => c.MKegiatan);

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