﻿using Application.Helpers;
using Domain.BUD;
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

namespace Application.BUD.SP2DDetBDanaCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdSP2DDetBDana { get; set; }
      public long? IdSP2DDetB { get; set; }
      public long? KdDana { get; set; }
      public decimal? Nilai { get; set; }
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
        var parameters = new List<Expression<Func<SP2DDetBDana, bool>>>();

        if (request.IdSP2DDetBDana.HasValue)
          parameters.Add(d => d.IdSP2DDetBDana == request.IdSP2DDetBDana);

        if (request.IdSP2DDetB.HasValue)
          parameters.Add(d => d.IdSP2DDetB == request.IdSP2DDetB);

        if (request.KdDana.HasValue)
          parameters.Add(d => d.KdDana == request.KdDana);

        if (request.Nilai.HasValue)
          parameters.Add(d => d.Nilai == request.Nilai);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.SP2DDetBDana.FindAll(predicate).Count();

        var result = await _context.SP2DDetBDana
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdSP2DDetBDana)
          .FindAllAsync<SP2DDetB, JDana>(
            predicate, x => x.SP2DDetB, x => x.JDana);

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