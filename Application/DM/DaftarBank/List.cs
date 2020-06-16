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

namespace Application.DM.DaftarBank
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public string KdBank { get; set; }
      public string AkBank { get; set; }
      public string Alamat { get; set; }
      public string Telepon { get; set; }
      public string Cabang { get; set; }
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
        var parameters = new List<Expression<Func<DaftBank, bool>>>();

        if (!string.IsNullOrWhiteSpace(request.KdBank))
          parameters.Add(d => d.KdBank.Contains(request.KdBank));

        if (!string.IsNullOrWhiteSpace(request.AkBank))
          parameters.Add(d => d.AkBank.Contains(request.AkBank));

        if (!string.IsNullOrWhiteSpace(request.Alamat))
          parameters.Add(d => d.Alamat.Contains(request.Alamat));

        if (!string.IsNullOrWhiteSpace(request.Telepon))
          parameters.Add(d => d.Telepon.Contains(request.Telepon));

        if (!string.IsNullOrWhiteSpace(request.Cabang))
          parameters.Add(d => d.Cabang.Contains(request.Cabang));

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.DaftBank.FindAll(predicate).Count();

        var result = await _context.DaftBank
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdBank)
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