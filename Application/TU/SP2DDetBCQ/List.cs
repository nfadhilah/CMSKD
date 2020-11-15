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

namespace Application.TU.SP2DDetBCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public string UnitKey { get; set; }
      public string NoSP2D { get; set; }
      public string MtgKey { get; set; }
      public string NoJeTra { get; set; }
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
        var parameter = new List<Expression<Func<SP2DDetB, bool>>>();

        if (!string.IsNullOrEmpty(request.UnitKey))
          parameter.Add(x => x.UnitKey == request.UnitKey);

        if (!string.IsNullOrWhiteSpace(request.NoSP2D))
          parameter.Add(x => x.NoSP2D.Contains(request.NoSP2D));

        if (!string.IsNullOrWhiteSpace(request.MtgKey))
          parameter.Add(x => x.MtgKey == request.MtgKey);

        if (!string.IsNullOrWhiteSpace(request.NoJeTra))
          parameter.Add(x => x.NoJeTra == request.NoJeTra);

        var predicate = PredicateBuilder.ComposeWithAnd(parameter);

        var totalItems = await _context.SP2DDetB.CountAsync(predicate);

        var result = await _context.SP2DDetB.SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, x => new { x.NoSP2D, x.MtgKey })
          .FindAllAsync<JDana, MatangB>(predicate, x => x.JDana, x => x.MatangB);

        return new PaginationWrapper
        {
          Data = _mapper.Map<IEnumerable<SP2DDetBDTO>>(result),
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