using Application.Dtos;
using Application.Helpers;
using Domain;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DafPajak
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public string NmPajak { get; set; }
      public string Uraian { get; set; }
      public string Keterangan { get; set; }
      public string RumusPajak { get; set; }
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
        var parameters = new List<Expression<Func<Pajak, bool>>>();

        if (!string.IsNullOrWhiteSpace(request.NmPajak))
          parameters.Add(d => d.NmPajak.Contains(request.NmPajak));

        if (!string.IsNullOrWhiteSpace(request.Uraian))
          parameters.Add(d => d.Uraian.Contains(request.Uraian));

        if (!string.IsNullOrWhiteSpace(request.Keterangan))
          parameters.Add(d => d.Keterangan.Contains(request.Keterangan));

        if (!string.IsNullOrWhiteSpace(request.RumusPajak))
          parameters.Add(d => d.RumusPajak == request.RumusPajak);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.Pajak.FindAll(predicate).Count();

        var result = await _context.Pajak
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdPjk)
          .FindAllAsync(predicate);

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