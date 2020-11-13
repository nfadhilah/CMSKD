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
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;

namespace Application.Auth.WebGroupCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
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
        var totalItems = await _context.WebGroup.CountAsync();

        var result = await _context.WebGroup.SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, x => x.GroupId).FindAllAsync();

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