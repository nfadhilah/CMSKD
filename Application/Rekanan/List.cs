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
using Application.Dtos;

namespace Application.Rekanan
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public string NmP3 { get; set; }
      public string NmInst { get; set; }
      public string NmBank { get; set; }
      public string JnsUsaha { get; set; }
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

        if (!string.IsNullOrWhiteSpace(request.NmP3))
          parameters.Add(d => d.NmP3.Contains(request.NmP3));

        if (!string.IsNullOrWhiteSpace(request.NmInst))
          parameters.Add(d => d.NmInst.Contains(request.NmInst));

        if (!string.IsNullOrWhiteSpace(request.NmBank))
          parameters.Add(d => d.NmBank.Contains(request.NmBank));

        if (!string.IsNullOrWhiteSpace(request.JnsUsaha))
          parameters.Add(d => d.JnsUsaha == request.JnsUsaha);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.DaftPhk3.FindAll(predicate).Count();

        var result = await _context.DaftPhk3
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.KdP3)
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