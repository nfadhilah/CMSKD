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
using Application.CommonDTO;

namespace Application.TUBEND.BkPajakDetStrCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdBkPajakStr { get; set; }
      public long? IdPajak { get; set; }
      public long? IdBkPajak { get; set; }
      public string IdBilling { get; set; }
      public DateTime? TglIdBilling { get; set; }
      public DateTime? TglExpire { get; set; }
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
        var parameters = new List<Expression<Func<BkPajakDetStr, bool>>>();

        if (request.IdBkPajakStr.HasValue)
          parameters.Add(d => d.IdBkPajakStr == request.IdBkPajakStr);

        if (!string.IsNullOrWhiteSpace(request.IdBilling))
          parameters.Add(d => d.IdBilling.Contains(request.IdBilling));

        if (!string.IsNullOrWhiteSpace(request.NTPN))
          parameters.Add(d => d.NTPN == request.NTPN);

        if (!string.IsNullOrWhiteSpace(request.NTB))
          parameters.Add(d => d.NTB == request.NTB);

        if (request.IdPajak.HasValue)
          parameters.Add(d => d.IdPajak == request.IdPajak);

        if (request.IdBkPajak.HasValue)
          parameters.Add(d => d.IdBkPajak == request.IdBkPajak);

        if (request.TglIdBilling.HasValue)
          parameters.Add(d => d.TglIdBilling == request.TglIdBilling);

        if (request.TglExpire.HasValue)
          parameters.Add(d => d.TglExpire == request.TglExpire);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.BkPajakDetStr.FindAll(predicate).Count();

        var result = await _context.BkPajakDetStr
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdBkPajakDetStr)
          .FindAllAsync<BPKPajakStr, Pajak, BkPajak>(predicate,
            x => x.BPKPajakStr, x => x.Pajak, x => x.BkPajak);

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