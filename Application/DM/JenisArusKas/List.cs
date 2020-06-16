using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Helpers;
using Domain.DM;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;

namespace Application.DM.JenisArusKas
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public string KdAKas { get; set; }
      public string NmAKas { get; set; }
      public string LabelKas { get; set; }
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
        var parameters = new List<Expression<Func<JAKas, bool>>>();

        if (!string.IsNullOrWhiteSpace(request.KdAKas))
          parameters.Add(d => d.KdAKas.Contains(request.KdAKas));

        if (!string.IsNullOrWhiteSpace(request.NmAKas))
          parameters.Add(d => d.NmAKas.Contains(request.NmAKas));

        if (!string.IsNullOrWhiteSpace(request.LabelKas))
          parameters.Add(d => d.LabelKas.Contains(request.LabelKas));

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.JAKas.FindAll(predicate).Count();

        var result = await _context.JAKas
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdKas)
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