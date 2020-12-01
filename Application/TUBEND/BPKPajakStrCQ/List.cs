﻿using Application.CommonDTO;
using Application.Helpers;
using AutoMapper;
using Domain.TUBEND;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.BPKPajakStrCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdBPKDetRP { get; set; }
    }

    public class Handler : IRequestHandler<Query, PaginationWrapper>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<PaginationWrapper> Handle(
        Query request, CancellationToken cancellationToken)
      {
        var parameters = new List<Expression<Func<BPKPajakStr, bool>>>();

        if (request.IdBPKDetRP.HasValue)
          parameters.Add(d => d.IdBPKDetRP == request.IdBPKDetRP);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.BPKPajakStr.FindAll(predicate).Count();

        var result = await _context.BPKPajakStr
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdBkPajakStr)
          .FindAllAsync<BPKDetRP>(
            predicate,
            x => x.BPKDetRp);

        return new PaginationWrapper(
          _mapper.Map<IEnumerable<BPKPajakStrDTO>>(result), new Pagination
          {
            CurrentPage = request.CurrentPage,
            PageSize = request.PageSize,
            TotalItemsCount = totalItemsCount
          });
      }
    }
  }
}