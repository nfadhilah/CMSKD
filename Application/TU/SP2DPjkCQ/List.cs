using Application.Common.DTOS;
using Application.Helpers;
using AutoMapper;
using Domain.DM;
using Domain.TU;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TU.SP2DPjkCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public string UnitKey { get; set; }
      public string NoSP2D { get; set; }
      public string PjkKey { get; set; }
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

      public async Task<PaginationWrapper> Handle(Query request, CancellationToken cancellationToken)
      {
        var parameter = new List<Expression<Func<SP2DPjk, bool>>>();

        if (!string.IsNullOrEmpty(request.UnitKey))
          parameter.Add(x => x.UnitKey == request.UnitKey);

        if (!string.IsNullOrWhiteSpace(request.NoSP2D))
          parameter.Add(x => x.NoSP2D.Contains(request.NoSP2D));

        if (!string.IsNullOrWhiteSpace(request.PjkKey))
          parameter.Add(x => x.PjkKey == request.PjkKey);

        var predicate = PredicateBuilder.ComposeWithAnd(parameter);

        var totalItems = await _context.SP2DPjk.CountAsync(predicate);

        var result = await _context.SP2DPjk.SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, x => new { x.NoSP2D, x.PjkKey })
          .FindAllAsync<JPajak>(predicate, x => x.JPajak);

        return new PaginationWrapper
        {
          Data = _mapper.Map<IEnumerable<SP2DPjkDTO>>(result),
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