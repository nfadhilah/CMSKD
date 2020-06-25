using Application.Dtos;
using Application.Helpers;
using Domain.DM;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.BendCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdPeg { get; set; }
      public string JnsBend { get; set; }
      public string RekBend { get; set; }
      public string IdBank { get; set; }
      public int? IdUnit { get; set; }
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
        var parameters = new List<Expression<Func<Bend, bool>>>();

        if (request.IdUnit.HasValue)
          parameters.Add(d => d.Pegawai.IdUnit == request.IdUnit);

        if (request.IdPeg.HasValue)
          parameters.Add(d => d.Pegawai.IdPeg == request.IdPeg);

        if (!string.IsNullOrWhiteSpace(request.JnsBend))
          parameters.Add(d => d.JnsBend.Contains(request.JnsBend));

        if (!string.IsNullOrWhiteSpace(request.RekBend))
          parameters.Add(d => d.RekBend.Contains(request.RekBend));

        if (!string.IsNullOrWhiteSpace(request.IdBank))
          parameters.Add(d => d.IdBank == request.IdBank);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.Bend.FindAll(predicate).Count();

        var result = await _context.Bend
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdBend)
          .FindAllAsync<Pegawai>(predicate, c => c.Pegawai);

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