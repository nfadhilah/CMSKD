using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Auth.WebUserCQ;
using Application.Common.DTOS;
using Application.Helpers;
using Application.Interfaces;
using AutoMapper;
using Domain.Auth;
using Domain.DM;
using Domain.TU;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;
using Persistence.Repository.TU;

namespace Application.TU.SP2DCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public string UnitKey { get; set; }
      public string NoSP2D { get; set; }
      public string KdStatus { get; set; }
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
        var parameter = new List<Expression<Func<SP2D, bool>>>();

        if (!string.IsNullOrEmpty(request.UnitKey))
          parameter.Add(x => x.UnitKey == request.UnitKey);

        if (!string.IsNullOrWhiteSpace(request.NoSP2D))
          parameter.Add(x => x.NoSP2D.Contains(request.NoSP2D));

        if (!string.IsNullOrWhiteSpace(request.KdStatus))
          parameter.Add(x => x.KdStatus == request.KdStatus);

        var predicate = PredicateBuilder.ComposeWithAnd(parameter);

        var totalItems = await _context.SP2D.CountAsync(predicate);

        var result = await _context.SP2D.SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, x => x.NoSP2D).FindAllAsync<DaftUnit>(predicate, x => x.DaftUnit);

        return new PaginationWrapper
        {
          Data = _mapper.Map<IEnumerable<SP2DDTO>>(result),
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