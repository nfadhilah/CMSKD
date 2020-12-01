using Application.Common.DTOS;
using Application.Helpers;
using Domain.DM;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.DaftDokCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public string KdDok { get; set; }
      public string NmDok { get; set; }
      public string Ket { get; set; }
    }

    public class Handler : IRequestHandler<Query, PaginationWrapper>
    {
      private readonly IDbContext _context;

      public Handler(IDbContext context)
      {
        _context = context;
      }

      public async Task<PaginationWrapper> Handle(Query request, CancellationToken cancellationToken)
      {
        var parameters = new List<Expression<Func<DaftDok, bool>>>();

        if (!string.IsNullOrWhiteSpace(request.KdDok))
          parameters.Add(x => x.KdDok == request.KdDok);

        if (!string.IsNullOrWhiteSpace(request.Ket))
          parameters.Add(x => x.NmDok.Contains(request.Ket));

        if (!string.IsNullOrWhiteSpace(request.NmDok))
          parameters.Add(x => x.NmDok == request.NmDok);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItems = await _context.DaftDok.CountAsync(predicate);

        var result = await _context.DaftDok.SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, x => x.KdDok)
          .FindAllAsync(predicate);

        return new PaginationWrapper
        {
          Data = result,
          Pagination = new Pagination
          {
            CurrentPage = request.CurrentPage,
            PageSize = request.PageSize,
            TotalItemsCount = totalItems
          }
        };
      }
    }
  }
}