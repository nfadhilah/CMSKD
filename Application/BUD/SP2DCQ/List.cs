using Application.Helpers;
using Domain.BUD;
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

namespace Application.BUD.SP2DCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public string NoSP2D { get; set; }
      public long? IdUnit { get; set; }
      public string KdStatus { get; set; }
      public long? IdSPM { get; set; }
      public string NoSPM { get; set; }
      public long? IdBend { get; set; }
      public long? IdSPD { get; set; }
      public long? IdPhk3 { get; set; }
      public long? IdTtd { get; set; }
      public int? IdxKode { get; set; }
      public string NoReg { get; set; }
      public string KetOtor { get; set; }
      public string NoKontrak { get; set; }
      public string Keperluan { get; set; }
      public string Penolakan { get; set; }
      public DateTime? TglValid { get; set; }
      public DateTime? TglSP2D { get; set; }
      public DateTime? TglSPM { get; set; }
      public string NoBBantu { get; set; }
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
        var parameters = new List<Expression<Func<SP2D, bool>>>();

        if (request.IdUnit.HasValue)
          parameters.Add(d => d.IdUnit == request.IdUnit);

        if (!string.IsNullOrWhiteSpace(request.NoKontrak))
          parameters.Add(d => d.NoKontrak.Contains(request.NoKontrak));

        if (!string.IsNullOrWhiteSpace(request.NoSPM))
          parameters.Add(d => d.NoSPM.Contains(request.NoSPM));

        if (!string.IsNullOrWhiteSpace(request.NoSP2D))
          parameters.Add(d => d.NoSP2D.Contains(request.NoSP2D));

        if (!string.IsNullOrWhiteSpace(request.KdStatus))
          parameters.Add(d => d.KdStatus == request.KdStatus);

        if (request.IdPhk3.HasValue)
          parameters.Add(d => d.IdPhk3 == request.IdPhk3);

        if (request.IdSPM.HasValue)
          parameters.Add(d => d.IdSPM == request.IdSPM);

        if (request.IdBend.HasValue)
          parameters.Add(d => d.IdBend == request.IdBend);

        if (request.IdSPD.HasValue)
          parameters.Add(d => d.IdSPD == request.IdSPD);

        if (request.IdxKode.HasValue)
          parameters.Add(d => d.IdxKode == request.IdxKode);

        if (request.IdTtd.HasValue)
          parameters.Add(d => d.IdTtd == request.IdTtd);

        if (request.TglSPM.HasValue)
          parameters.Add(d => d.TglSPM == request.TglSPM);

        if (request.TglSP2D.HasValue)
          parameters.Add(d => d.TglSP2D == request.TglSP2D);

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

        if (!string.IsNullOrWhiteSpace(request.NoBBantu))
          parameters.Add(d => d.NoBBantu == request.NoBBantu);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.SP2D.FindAll(predicate).Count();

        var result = await _context.SP2D
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.NoSPM)
          .FindAllAsync<DaftUnit, Bend, SPD, DaftPhk3, JabTtd>(
            predicate, x => x.Unit, x => x.Bend, x => x.SPD,
            x => x.Phk3, x => x.JabTtd);

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