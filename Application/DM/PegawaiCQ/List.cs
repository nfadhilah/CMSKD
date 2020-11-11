using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.CommonDTO;
using Application.Helpers;
using Domain.DM;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;

namespace Application.DM.PegawaiCQ
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

      public Handler(IDbContext context)
      {
        _context = context;
      }

      public async Task<PaginationWrapper> Handle(Query request, CancellationToken cancellationToken)
      {
        var parameters = new List<Expression<Func<Pegawai, bool>>>();

        if (!string.IsNullOrWhiteSpace(request.UnitKey))
          parameters.Add(x => x.UnitKey == request.UnitKey);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItems = await _context.Pegawai.CountAsync(predicate);

        var result = await _context.Pegawai.SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, x => x.Nama)
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