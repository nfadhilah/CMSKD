using Application.Common.DTOS;
using Application.Helpers;
using AutoMapper;
using Domain.Auth;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Auth.WebUserCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public string UnitKey { get; set; }
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
        var parameter = new List<Expression<Func<WebUser, bool>>>();

        if (!string.IsNullOrEmpty(request.UnitKey))
          parameter.Add(x => x.UnitKey == request.UnitKey);

        var predicate = PredicateBuilder.ComposeWithAnd(parameter);

        var totalItems = await _context.WebUser.CountAsync(predicate);

        var result = await _context.WebUser.SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, x => x.UserId).FindAllAsync<WebGroup>(predicate, x => x.WebGroup);

        return new PaginationWrapper
        {
          Data = _mapper.Map<IEnumerable<WebUserDTO>>(result),
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