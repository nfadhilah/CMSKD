using Application.Dtos;
using Application.Helpers;
using Domain.BUD;
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

namespace Application.BUD.SP2DDetRPCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdSP2DDetR { get; set; }
      public long? IdPajak { get; set; }
      public decimal? Nilai { get; set; }
      public string Keterangan { get; set; }
      public string IdBilling { get; set; }
      public DateTime? TglBilling { get; set; }
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
        var parameters = new List<Expression<Func<SP2DDetRP, bool>>>();

        if (request.IdPajak.HasValue)
          parameters.Add(d => d.IdPajak == request.IdPajak);

        if (request.IdSP2DDetR.HasValue)
          parameters.Add(d => d.IdSP2DDetR == request.IdSP2DDetR);

        if (request.Nilai.HasValue)
          parameters.Add(d => d.Nilai == request.Nilai);

        if (request.TglBilling.HasValue)
          parameters.Add(d => d.TglBilling == request.TglBilling);

        if (!string.IsNullOrWhiteSpace(request.IdBilling))
          parameters.Add(d => d.IdBilling == request.IdBilling);

        if (!string.IsNullOrWhiteSpace(request.Keterangan))
          parameters.Add(d => d.Keterangan.Contains(request.Keterangan));

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.SP2DDetRP.FindAll(predicate).Count();

        var result = await _context.SP2DDetRP
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdSP2DDetRP)
          .FindAllAsync<SP2DDetR, Pajak>(
            predicate, x => x.SP2DDetR,
            x => x.Pajak);

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