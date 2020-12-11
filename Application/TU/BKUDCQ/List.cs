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

namespace Application.TU.BKUDCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public string NoBuKas { get; set; }
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
        var parameters = new List<Expression<Func<BKUD, bool>>>();

        if (!string.IsNullOrWhiteSpace(request.NoBuKas))
          parameters.Add(x => x.UnitKey == request.NoBuKas);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItems = await _context.BKUD.CountAsync();

        var result = await _context.BKUD.SetLimit(request.Limit, request.Offset)
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