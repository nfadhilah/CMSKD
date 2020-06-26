using Application.Helpers;
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

namespace Application.MA.DPADanaRCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdDPAR { get; set; }
      public string KdDana { get; set; }
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
        var parameters = new List<Expression<Func<DPADanaR, bool>>>();

        if (request.IdDPAR.HasValue)
          parameters.Add(d => d.IdDPAR == request.IdDPAR);
        if (!string.IsNullOrWhiteSpace(request.KdDana))
          parameters.Add(d => d.KdDana == request.KdDana);
        if (request.Nilai.HasValue)
          parameters.Add(d => d.Nilai == request.Nilai);
        if (request.DateCreate.HasValue)
          parameters.Add(d => d.DateCreate == request.DateCreate);
        if (request.DateUpdate.HasValue)
          parameters.Add(d => d.DateUpdate == request.DateUpdate);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.DPADanaR.FindAll(predicate).Count();

        var result = await _context.DPADanaR
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdDPADanaR)
          .FindAllAsync<DPAR>(predicate, c => c.DPAR);

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