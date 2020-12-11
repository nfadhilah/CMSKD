using Application.Common.DTOS;
using Application.Helpers;
using Domain.TU;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TU.BKUSTSCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public string UnitKey { get; set; }
      public string NoBKUSKPD { get; set; }
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
        var parameters = new List<Expression<Func<BKUSTS, bool>>>();

        if (!string.IsNullOrWhiteSpace(request.UnitKey))
          parameters.Add(x => x.UnitKey == request.UnitKey);

        if (!string.IsNullOrWhiteSpace(request.NoBKUSKPD))
          parameters.Add(x => x.NoBKUSKPD == request.NoBKUSKPD);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItems = await _context.BKUSTS.CountAsync(predicate);

        var result = await _context.BKUSTS.SetLimit(request.Limit, request.Offset)
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