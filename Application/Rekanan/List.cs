using Application.ResponseWrapper;
using Domain;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Rekanan
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public string Nmp3 { get; set; }
      public string Nminst { get; set; }
      public string Norcp3 { get; set; }
      public string Nmbank { get; set; }
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
        var parameters = new List<Expression<Func<DaftPhk3, bool>>>();

        if (!string.IsNullOrWhiteSpace(request.Nmp3))
          parameters.Add(d => d.Nmp3 == request.Nmp3);

        if (!string.IsNullOrWhiteSpace(request.Norcp3))
          parameters.Add(d => d.Norcp3 == request.Norcp3);

        if (!string.IsNullOrWhiteSpace(request.Nminst))
          parameters.Add(d => d.Nminst == request.Nminst);

        if (!string.IsNullOrWhiteSpace(request.Nmbank))
          parameters.Add(d => d.Nmbank == request.Nmbank);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var result = await _context.DaftPhk3
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.Kdp3)
          .FindAllAsync(predicate);

        return new PaginationWrapper(result, new Pagination
        {
          CurrentPage = request.CurrentPage,
          PageSize = request.PageSize,
          TotalItemsCount = _context.DaftPhk3.Count()
        });
      }
    }
  }
}