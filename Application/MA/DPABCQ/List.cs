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

namespace Application.MA.DPABCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdDPA { get; set; }
      public int? IdXKode { get; set; }
      public string KdTahap { get; set; }
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
        var parameters = new List<Expression<Func<DPAB, bool>>>();

        if (request.IdDPA.HasValue)
          parameters.Add(d => d.IdDPA == request.IdDPA);
        if (request.IdXKode.HasValue)
          parameters.Add(d => d.IdXKode == request.IdXKode);
        if (!string.IsNullOrWhiteSpace(request.KdTahap))
          parameters.Add(x => x.KdTahap == request.KdTahap);
        if (request.IdRek.HasValue)
          parameters.Add(d => d.IdRek == request.IdRek);
        if (request.Nilai.HasValue)
          parameters.Add(d => d.Nilai == request.Nilai);
        if (request.DateCreate.HasValue)
          parameters.Add(d => d.DateCreate == request.DateCreate);
        if (request.DateUpdate.HasValue)
          parameters.Add(d => d.DateUpdate == request.DateUpdate);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.DPAB.FindAll(predicate).Count();

        var result = await _context.DPAB
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdDPAB)
          .FindAllAsync<DPA, DaftRekening>(predicate, c => c.DPA, c => c.DaftRekening);

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