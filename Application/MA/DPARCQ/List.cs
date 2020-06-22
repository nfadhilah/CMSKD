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

namespace Application.MA.DPARCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdDPA { get; set; }
      public string KdTahap { get; set; }
      public int? IdXKode { get; set; }
      public long? IdKeg { get; set; }
      public long? IdRek { get; set; }
      public Decimal? Nilai { get; set; }
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
        var parameters = new List<Expression<Func<DPAR, bool>>>();

        if (request.IdDPA.HasValue)
          parameters.Add(d => d.IdDPA == request.IdDPA);
        if (!string.IsNullOrWhiteSpace(request.KdTahap))
          parameters.Add(x => x.KdTahap == request.KdTahap);
        if (request.IdXKode.HasValue)
          parameters.Add(d => d.IdXKode == request.IdXKode);
        if (request.IdKeg.HasValue)
          parameters.Add(d => d.IdKeg == request.IdKeg);
        if (request.IdRek.HasValue)
          parameters.Add(d => d.IdRek == request.IdRek);
        if (request.Nilai.HasValue)
          parameters.Add(d => d.Nilai == request.Nilai);
        if (request.DateCreate.HasValue)
          parameters.Add(d => d.DateCreate == request.DateCreate);
        if (request.DateUpdate.HasValue)
          parameters.Add(d => d.DateUpdate == request.DateUpdate);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.DPAR.FindAll(predicate).Count();

        var result = await _context.DPAR
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdDPAR)
          .FindAllAsync<DPA, DaftRekening, KegUnit>(predicate, c => c.DPA, c => c.DaftRekening, c => c.KegUnit);

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