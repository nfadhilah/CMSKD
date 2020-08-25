using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.CommonDTO;
using Application.Helpers;
using Application.PM.PaketRUPCQ;
using AutoMapper;
using Domain.DM;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;

namespace Application.PM.PaketRUPDetCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdRUPDet { get; set; }
      public long? IdRUP { get; set; }
      public string KodeRUP { get; set; }
      public string NmPaket { get; set; }
      public string Lokasi { get; set; }
      public string Volume { get; set; }
      public string UraiPaket { get; set; }
      public long? IdJnsPekerjaan { get; set; }
      public DateTime? AwalPekerjaan { get; set; }
      public DateTime? AkhirPekerjaan { get; set; }
      public long? IdJDana { get; set; }
      public long? IdPhk3 { get; set; }
    }

    public class Handler : IRequestHandler<Query, PaginationWrapper>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<PaginationWrapper> Handle(
        Query request, CancellationToken cancellationToken)
      {
        var parameters =
          new List<Expression<Func<Domain.PM.PaketRUPDet, bool>>>();

        if (request.IdRUPDet.HasValue)
          parameters.Add(x => x.IdRUP == request.IdRUP);

        if (request.IdRUP.HasValue)
          parameters.Add(x => x.IdRUP == request.IdRUP);

        if (!string.IsNullOrWhiteSpace(request.KodeRUP))
          parameters.Add(x => x.KodeRUP == request.KodeRUP);

        if (!string.IsNullOrWhiteSpace(request.NmPaket))
          parameters.Add(x => x.NmPaket.Contains(request.NmPaket));

        if (!string.IsNullOrWhiteSpace(request.Lokasi))
          parameters.Add(x => x.Lokasi.Contains(request.Lokasi));

        if (!string.IsNullOrWhiteSpace(request.Volume))
          parameters.Add(x => x.Volume.Contains(request.Volume));

        if (!string.IsNullOrWhiteSpace(request.UraiPaket))
          parameters.Add(x => x.UraiPaket.Contains(request.UraiPaket));

        if (request.AwalPekerjaan.HasValue)
          parameters.Add(x => x.AwalPekerjaan == request.AwalPekerjaan);

        if (request.AkhirPekerjaan.HasValue)
          parameters.Add(x => x.AkhirPekerjaan == request.AkhirPekerjaan);

        if (request.IdJDana.HasValue)
          parameters.Add(x => x.IdJDana == request.IdJDana);

        if (request.IdJnsPekerjaan.HasValue)
          parameters.Add(x => x.IdJnsPekerjaan == request.IdJnsPekerjaan);

        if (request.IdPhk3.HasValue)
          parameters.Add(x => x.IdPhk3 == request.IdPhk3);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.PaketRupDet.FindAll().Count();

        var result = await _context.PaketRupDet
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdRUPDet)
          .FindAllAsync<JPekerjaan, JDana, DaftPhk3>(predicate,
            c => c.JnsPekerjaan,
            c => c.JDana, c => c.Phk3);

        return new PaginationWrapper(
          _mapper.Map<IEnumerable<PaketRUPDetDTO>>(result),
          new Pagination
          {
            CurrentPage = request.CurrentPage,
            PageSize = request.PageSize,
            TotalItemsCount = totalItemsCount
          });
      }
    }
  }
}