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

namespace Application.TUBEND.SPPDetRPCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdSPPDetR { get; set; }
      public long? IdPajak { get; set; }
      public string Keterangan { get; set; }
      public string IdBilling { get; set; }
      public DateTime? TglBilling { get; set; }
      public string NTPN { get; set; }
      public string NTB { get; set; }
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
        var parameters = new List<Expression<Func<SPPDetRP, bool>>>();

        if (request.IdSPPDetR.HasValue)
          parameters.Add(d => d.IdSPPDetR == request.IdSPPDetR);

        if (request.IdPajak.HasValue)
          parameters.Add(d => d.IdPajak == request.IdPajak);

        if (!string.IsNullOrWhiteSpace(request.Keterangan))
          parameters.Add(d => d.Keterangan.Contains(request.Keterangan));

        if (!string.IsNullOrWhiteSpace(request.IdBilling))
          parameters.Add(d => d.IdBilling.Contains(request.IdBilling));

        if (request.TglBilling.HasValue)
          parameters.Add(d => d.TglBilling == request.TglBilling);

        if (!string.IsNullOrWhiteSpace(request.NTPN))
          parameters.Add(d => d.NTPN.Contains(request.NTPN));

        if (!string.IsNullOrWhiteSpace(request.NTB))
          parameters.Add(d => d.NTB.Contains(request.NTB));

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.SPPDetRP.FindAll(predicate).Count();

        var result = await _context.SPPDetRP
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdSPPDetRP)
          .FindAllAsync<SPPDetR, Pajak>(
            predicate, x => x.SPPDetR,
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