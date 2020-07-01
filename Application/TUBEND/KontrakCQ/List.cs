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

namespace Application.TUBEND.KontrakCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdUnit { get; set; }
      public string NoKontrak { get; set; }
      public long? IdPhk3 { get; set; }
      public long? IdKeg { get; set; }
      public DateTime? TglKon { get; set; }
      public DateTime? TglAwalKontrak { get; set; }
      public DateTime? TglAkhirKontrak { get; set; }
      public DateTime? TglSlsKontrak { get; set; }
      public string Uraian { get; set; }
      public decimal? Nilai { get; set; }
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
        var parameters = new List<Expression<Func<Kontrak, bool>>>();

        if (request.IdUnit.HasValue)
          parameters.Add(d => d.IdUnit == request.IdUnit);

        if (!string.IsNullOrWhiteSpace(request.NoKontrak))
          parameters.Add(d => d.NoKontrak.Contains(request.NoKontrak));

        if (request.IdPhk3.HasValue)
          parameters.Add(d => d.IdPhk3 == request.IdPhk3);

        if (request.IdKeg.HasValue)
          parameters.Add(d => d.IdKeg == request.IdKeg);

        if (request.TglAwalKontrak.HasValue)
          parameters.Add(d => d.TglAwalKontrak == request.TglAwalKontrak);

        if (request.TglKon.HasValue)
          parameters.Add(d => d.TglKon == request.TglKon);

        if (request.TglAkhirKontrak.HasValue)
          parameters.Add(d => d.TglAkhirKontrak == request.TglAkhirKontrak);

        if (request.TglSlsKontrak.HasValue)
          parameters.Add(d => d.TglSlsKontrak == request.TglSlsKontrak);

        if (request.IdKeg.HasValue)
          parameters.Add(d => d.IdKeg == request.IdKeg);

        if (!string.IsNullOrWhiteSpace(request.Uraian))
          parameters.Add(d => d.NoKontrak.Contains(request.Uraian));


        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.Kontrak.FindAll(predicate).Count();

        var result = await _context.Kontrak
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.NoKontrak)
          .FindAllAsync<DaftUnit, DaftPhk3, MKegiatan>(predicate, x => x.Unit,
            x => x.Phk3, x => x.Kegiatan);

        return new PaginationWrapper(
          _mapper.Map<IEnumerable<KontrakDTO>>(result), new Pagination
          {
            CurrentPage = request.CurrentPage,
            PageSize = request.PageSize,
            TotalItemsCount = totalItemsCount
          });
      }
    }
  }
}