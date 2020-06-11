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

namespace Application.JenisSatuan
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public string KdSatuan { get; set; }
      public string UraiSatuan { get; set; }
      public string Ket { get; set; }
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
        var parameters = new List<Expression<Func<JSatuan, bool>>>();

        if (!string.IsNullOrWhiteSpace(request.KdSatuan))
          parameters.Add(d => d.KdSatuan.Contains(request.KdSatuan));

        if (!string.IsNullOrWhiteSpace(request.UraiSatuan))
          parameters.Add(d => d.UraiSatuan.Contains(request.UraiSatuan));

        if (!string.IsNullOrWhiteSpace(request.Ket))
          parameters.Add(d => d.Ket.Contains(request.Ket));

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.JSatuan.FindAll(predicate).Count();

        var result = await _context.JSatuan
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdSatuan)
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