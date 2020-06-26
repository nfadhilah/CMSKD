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
using Application.CommonDTO;

namespace Application.TUBEND.SPMCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdUnit { get; set; }
      public string NoSPM { get; set; }
      public string KdStatus { get; set; }
      public long? IdBend { get; set; }
      public long? IdSPD { get; set; }
      public long? IdSPP { get; set; }
      public long? IdPhk3 { get; set; }
      public int? IdxKode { get; set; }
      public string NoReg { get; set; }
      public string KetOtor { get; set; }
      public string NoKontrak { get; set; }
      public string Keperluan { get; set; }
      public string Penolakan { get; set; }
      public DateTime? TglValid { get; set; }
      public DateTime? TglSPM { get; set; }
      public DateTime? TglSPP { get; set; }
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
        var parameters = new List<Expression<Func<SPM, bool>>>();

        if (request.IdUnit.HasValue)
          parameters.Add(d => d.IdUnit == request.IdUnit);

        if (!string.IsNullOrWhiteSpace(request.NoKontrak))
          parameters.Add(d => d.NoKontrak.Contains(request.NoKontrak));

        if (!string.IsNullOrWhiteSpace(request.NoSPM))
          parameters.Add(d => d.NoSPM.Contains(request.NoSPM));

        if (!string.IsNullOrWhiteSpace(request.KdStatus))
          parameters.Add(d => d.KdStatus == request.KdStatus);

        if (request.IdPhk3.HasValue)
          parameters.Add(d => d.IdPhk3 == request.IdPhk3);

        if (request.IdSPP.HasValue)
          parameters.Add(d => d.IdSPP == request.IdSPP);

        if (request.IdBend.HasValue)
          parameters.Add(d => d.IdBend == request.IdBend);

        if (request.IdSPD.HasValue)
          parameters.Add(d => d.IdSPD == request.IdSPD);

        if (request.IdxKode.HasValue)
          parameters.Add(d => d.IdxKode == request.IdxKode);

        if (request.TglSPP.HasValue)
          parameters.Add(d => d.TglSPP == request.TglSPP);

        if (request.TglSPM.HasValue)
          parameters.Add(d => d.TglSPM == request.TglSPM);

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

        var totalItemsCount = _context.SPM.FindAll(predicate).Count();

        var result = await _context.SPM
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.NoSPM)
          .FindAllAsync<DaftUnit, Bend, SPD, SPP, DaftPhk3>(
            predicate, x => x.Unit, x => x.Bend, x => x.SPD,
            x => x.SPP, x => x.Phk3);

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