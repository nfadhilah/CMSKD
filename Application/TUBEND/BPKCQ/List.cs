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

namespace Application.TUBEND.BPKCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdUnit { get; set; }
      public long? IdPhk3 { get; set; }
      public string NoBPK { get; set; }
      public string KdStatus { get; set; }
      public long? IdJBayar { get; set; }
      public int? IdxKode { get; set; }
      public long? IdBend { get; set; }
      public DateTime? TglBPK { get; set; }
      public string UraiBPK { get; set; }
      public DateTime? TglValid { get; set; }
      public long? IdBerita { get; set; }
      public long? KdRilis { get; set; }
      public int? StKirim { get; set; }
      public int? StCair { get; set; }
      public string NoRef { get; set; }
      public DateTime? DateCreate { get; set; }
      public bool? IsValid { get; set; }
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
        var parameters = new List<Expression<Func<BPK, bool>>>();

        if (request.IdUnit.HasValue)
          parameters.Add(d => d.IdUnit == request.IdUnit);

        if (!string.IsNullOrWhiteSpace(request.NoBPK))
          parameters.Add(d => d.NoBPK.Contains(request.NoBPK));

        if (request.IdJBayar.HasValue)
          parameters.Add(d => d.IdJBayar == request.IdJBayar);

        if (!string.IsNullOrWhiteSpace(request.KdStatus))
          parameters.Add(d => d.KdStatus == request.KdStatus);

        if (!string.IsNullOrWhiteSpace(request.NoRef))
          parameters.Add(d => d.NoRef == request.NoRef);

        if (request.IdPhk3.HasValue)
          parameters.Add(d => d.IdPhk3 == request.IdPhk3);

        if (request.StKirim.HasValue)
          parameters.Add(d => d.StKirim == request.StKirim);

        if (request.StCair.HasValue)
          parameters.Add(d => d.StCair == request.StCair);

        if (request.TglBPK.HasValue)
          parameters.Add(d => d.TglBPK == request.TglBPK);

        if (request.IdBend.HasValue)
          parameters.Add(d => d.IdBend == request.IdBend);

        if (request.IdBerita.HasValue)
          parameters.Add(d => d.IdBerita == request.IdBerita);

        if (request.IdxKode.HasValue)
          parameters.Add(d => d.IdxKode == request.IdxKode);

        if (request.KdRilis.HasValue)
          parameters.Add(d => d.KdRilis == request.KdRilis);

        if (request.TglValid.HasValue)
          parameters.Add(d => d.TglValid == request.TglValid);

        if (request.IsValid.HasValue)
          parameters.Add(d => d.TglValid != null);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.BPK.FindAll(predicate).Count();

        var result = await _context.BPK
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.NoBPK)
          .FindAllAsync<DaftUnit, DaftPhk3, Bend, Berita, JBayar>(
            predicate, x => x.Unit,
            x => x.Phk3, x => x.Bend, x => x.Berita, x => x.JBayar);

        return new PaginationWrapper(_mapper.Map<IEnumerable<BPKDTO>>(result),
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