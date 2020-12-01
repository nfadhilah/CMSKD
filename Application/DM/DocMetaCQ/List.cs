using Application.Common.DTOS;
using Application.Helpers;
using Domain.DM;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.DocMetaCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public string KdDok { get; set; }
      public string NoDok { get; set; }
      public string FilePath { get; set; }
      public int? Status { get; set; }
      public DateTime? DateCreated { get; set; }
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
        var parameters = new List<Expression<Func<DocMeta, bool>>>();

        if (!string.IsNullOrWhiteSpace(request.KdDok))
          parameters.Add(x => x.KdDok == request.KdDok);

        if (!string.IsNullOrWhiteSpace(request.NoDok))
          parameters.Add(x => x.NoDok == request.NoDok);

        if (!string.IsNullOrWhiteSpace(request.FilePath))
          parameters.Add(x => x.NoDok == request.FilePath);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItems = await _context.DocMeta.CountAsync(predicate);

        var result = await _context.DocMeta.SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, x => x.KdDok)
          .FindAllAsync(predicate);

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