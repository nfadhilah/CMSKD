using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.CommonDTO;
using Application.Helpers;
using Application.Interfaces;
using Domain.Auth;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;

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
      private readonly IUserAccessor _userAccessor;

      public Handler(IDbContext context, IUserAccessor userAccessor)
      {
        _context = context;
        _userAccessor = userAccessor;
      }

      public async Task<PaginationWrapper> Handle(Query request, CancellationToken cancellationToken)
      {
        var parameter = new List<Expression<Func<WebUser, bool>>>();

        if (!string.IsNullOrEmpty(request.UnitKey))
          parameter.Add(x => x.UnitKey == request.UnitKey);

        var predicate = PredicateBuilder.ComposeWithAnd(parameter);

        var totalItems = await _context.WebUser.CountAsync(predicate);

        var result = await _context.WebUser.SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, x => x.UserId).FindAllAsync(predicate);

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