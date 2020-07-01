using Application.CommonDTO;
using Application.Helpers;
using AutoMapper;
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

namespace Application.TUBEND.BeritaCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdUnit { get; set; }
      public long? IdKeg { get; set; }
      public string NoBerita { get; set; }
      public DateTime? TglBA { get; set; }
      public long? IdKontrak { get; set; }
      public string Urai_Berita { get; set; }
      public DateTime? TglValid { get; set; }
      public string KdStatus { get; set; }
      public DateTime? DateCreate { get; set; }
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
        var parameters = new List<Expression<Func<Berita, bool>>>();

        if (request.IdUnit.HasValue)
          parameters.Add(d => d.IdUnit == request.IdUnit);

        if (!string.IsNullOrWhiteSpace(request.NoBerita))
          parameters.Add(d => d.NoBerita.Contains(request.NoBerita));

        if (request.IdKeg.HasValue)
          parameters.Add(d => d.IdKeg == request.IdKeg);

        if (request.TglBA.HasValue)
          parameters.Add(d => d.TglBA == request.TglBA);

        if (request.IdKontrak.HasValue)
          parameters.Add(d => d.IdKontrak == request.IdKontrak);

        if (request.TglValid.HasValue)
          parameters.Add(d => d.TglValid == request.TglValid);

        if (request.IdKeg.HasValue)
          parameters.Add(d => d.IdKeg == request.IdKeg);

        if (!string.IsNullOrWhiteSpace(request.Urai_Berita))
          parameters.Add(d => d.Urai_Berita.Contains(request.Urai_Berita));

        if (!string.IsNullOrWhiteSpace(request.KdStatus))
          parameters.Add(d => d.KdStatus.Contains(request.KdStatus));

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.Berita.FindAll(predicate).Count();

        var result = await _context.Berita
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.NoBerita)
          .FindAllAsync<DaftUnit, MKegiatan, Kontrak>(predicate, x => x.Unit,
            x => x.Kegiatan, x => x.Kontrak);

        return new PaginationWrapper(
          _mapper.Map<IEnumerable<BeritaDTO>>(result), new Pagination
          {
            CurrentPage = request.CurrentPage,
            PageSize = request.PageSize,
            TotalItemsCount = totalItemsCount
          });
      }
    }
  }
}