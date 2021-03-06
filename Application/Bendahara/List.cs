﻿using Application.Dtos;
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

namespace Application.Bendahara
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public string NIP { get; set; }
      public string Jns_Bend { get; set; }
      public string RekBend { get; set; }
      public string KdBank { get; set; }
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
        var parameters = new List<Expression<Func<Bend, bool>>>();

        if (!string.IsNullOrWhiteSpace(request.NIP))
          parameters.Add(d => d.NIP.Contains(request.NIP));

        if (!string.IsNullOrWhiteSpace(request.Jns_Bend))
          parameters.Add(d => d.Jns_Bend.Contains(request.Jns_Bend));

        if (!string.IsNullOrWhiteSpace(request.RekBend))
          parameters.Add(d => d.RekBend.Contains(request.RekBend));

        if (!string.IsNullOrWhiteSpace(request.KdBank))
          parameters.Add(d => d.KdBank == request.KdBank);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.Bend.FindAll(predicate).Count();

        var result = await _context.Bend
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.KeyBend)
          .FindAllAsync<Pegawai>(predicate, c => c.Pegawai);

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