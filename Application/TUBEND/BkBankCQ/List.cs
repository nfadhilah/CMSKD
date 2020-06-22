using Application.Dtos;
using Application.Helpers;
using Domain.DM;
using Domain.TUBEND;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.BkBankCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdUnit { get; set; }
      public long? IdBend { get; set; }
      public string NoBuku { get; set; }
      public string KdStatus { get; set; }
      public DateTime? TglBuku { get; set; }
      public DateTime? TglValid { get; set; }
      public string Uraian { get; set; }
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
        var parameters = new List<Expression<Func<BkBank, bool>>>();

        if (request.IdUnit.HasValue)
          parameters.Add(d => d.IdUnit == request.IdUnit);

        if (!string.IsNullOrWhiteSpace(request.NoBuku))
          parameters.Add(d => d.NoBuku.Contains(request.NoBuku));

        if (request.IdBend.HasValue)
          parameters.Add(d => d.IdBend == request.IdBend);

        if (request.TglBuku.HasValue)
          parameters.Add(d => d.TglBuku == request.TglBuku);

        if (request.TglValid.HasValue)
          parameters.Add(d => d.TglValid == request.TglValid);

        if (!string.IsNullOrWhiteSpace(request.Uraian))
          parameters.Add(d => d.Uraian.Contains(request.Uraian));

        if (!string.IsNullOrWhiteSpace(request.KdStatus))
          parameters.Add(d => d.Uraian == request.Uraian);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.BkBank.FindAll(predicate).Count();

        var result = await _context.BkBank
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.NoBuku)
          .FindAllAsync<DaftUnit, Bend, StatTrs>(predicate, x => x.Unit,
            x => x.Bend, x => x.StatTrs);

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