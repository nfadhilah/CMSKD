﻿using Application.Helpers;
using Domain.DM;
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

namespace Application.DM.JTransCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public string NmTrans { get; set; }
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
        var parameters = new List<Expression<Func<JTrans, bool>>>();

        if (!string.IsNullOrWhiteSpace(request.NmTrans))
          parameters.Add(d => d.NmTrans.Contains(request.NmTrans));

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.JTrans.FindAll(predicate).Count();

        var result = await _context.JTrans
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdTrans)
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