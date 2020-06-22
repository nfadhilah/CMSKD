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

namespace Application.MA.PgrmUnitCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdUnit { get; set; }
      public string KdTahap { get; set; }
      public long? IdPrgrm { get; set; }
      public string Target { get; set; }
      public string Sasaran { get; set; }
      public int? NoPrio { get; set; }
      public string Indikator { get; set; }
      public string Ket { get; set; }
      public string IdSas { get; set; }
      public DateTime? TglValid { get; set; }
      public int? IdXKode { get; set; }
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
        var parameters = new List<Expression<Func<PgrmUnit, bool>>>();

        if (request.IdUnit.HasValue)
          parameters.Add(d => d.IdUnit == request.IdUnit);
        if (!string.IsNullOrWhiteSpace(request.KdTahap))
          parameters.Add(d => d.KdTahap == request.KdTahap);
        if (request.IdPrgrm.HasValue)
          parameters.Add(x => x.IdPrgrm == request.IdPrgrm);
        if (!string.IsNullOrWhiteSpace(request.Target))
          parameters.Add(d => d.Target == request.Target);
        if (!string.IsNullOrWhiteSpace(request.Sasaran))
          parameters.Add(d => d.Sasaran == request.Sasaran);
        if (request.NoPrio.HasValue)
          parameters.Add(x => x.NoPrio == request.NoPrio);
        if (!string.IsNullOrWhiteSpace(request.Indikator))
          parameters.Add(d => d.Indikator == request.Indikator);
        if (!string.IsNullOrWhiteSpace(request.Ket))
          parameters.Add(d => d.Ket == request.Ket);
        if (!string.IsNullOrWhiteSpace(request.IdSas))
          parameters.Add(d => d.IdSas == request.IdSas);
        if (request.TglValid.HasValue)
          parameters.Add(d => d.TglValid == request.TglValid);
        if (request.IdXKode.HasValue)
          parameters.Add(x => x.IdXKode == request.IdXKode);
        if (request.DateCreate.HasValue)
          parameters.Add(d => d.DateCreate == request.DateCreate);
        if (request.DateUpdate.HasValue)
          parameters.Add(d => d.DateUpdate == request.DateUpdate);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.PgrmUnit.FindAll(predicate).Count();

        var result = await _context.PgrmUnit
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdPgrmUnit)
          .FindAllAsync<MPgrm>(predicate, c => c.MPgrm);

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