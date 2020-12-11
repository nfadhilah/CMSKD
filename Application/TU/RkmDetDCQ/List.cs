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

namespace Application.TU.RkmDetDCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public string UnitKey { get; set; }
      public string NoSTS { get; set; }
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
        var parameters = new List<Expression<Func<RkmDetD, bool>>>();

        if (!string.IsNullOrWhiteSpace(request.UnitKey))
          parameters.Add(x => x.UnitKey == request.UnitKey);

        if (!string.IsNullOrWhiteSpace(request.NoSTS))
          parameters.Add(x => x.NoSTS == request.NoSTS);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItems = await _context.RkmDetD.CountAsync(predicate);

        var result = await _context.RkmDetD.SetLimit(request.Limit, request.Offset)
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