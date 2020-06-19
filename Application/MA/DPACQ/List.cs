using Application.Dtos;
using Application.Helpers;
using Domain.DM;
using Domain.MA;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MA.DPACQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdUnit { get; set; }
      public string NoDPA { get; set; }
      public DateTime? TglDPA { get; set; }
      public string NoSah { get; set; }
      public string Keterangan { get; set; }
      public DateTime? TglValid { get; set; }
      public DateTime? DateCreate { get; set; }
      public DateTime? DateUpdate { get; set; }
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
        var parameters = new List<Expression<Func<DPA, bool>>>();

        if (request.IdUnit.HasValue)
          parameters.Add(d => d.IdUnit == request.IdUnit.Value);
        if (!string.IsNullOrWhiteSpace(request.NoDPA))
          parameters.Add(d => d.NoDPA == request.NoDPA);
        if (request.TglDPA.HasValue)
          parameters.Add(x => x.TglDPA == request.TglDPA);
        if (!string.IsNullOrWhiteSpace(request.NoSah))
          parameters.Add(d => d.NoSah == request.NoSah);
        if (!string.IsNullOrWhiteSpace(request.Keterangan))
          parameters.Add(d => d.Keterangan == request.Keterangan);
        if (request.TglValid.HasValue)
          parameters.Add(d => d.TglValid == request.TglValid);
        if (request.DateCreate.HasValue)
          parameters.Add(d => d.DateCreate == request.DateCreate);
        if (request.DateUpdate.HasValue)
          parameters.Add(d => d.DateUpdate == request.DateUpdate);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.DPA.FindAll(predicate).Count();

        var result = await _context.DPA
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdDPA)
          .FindAllAsync<DaftUnit>(predicate, c => c.DaftUnit);

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