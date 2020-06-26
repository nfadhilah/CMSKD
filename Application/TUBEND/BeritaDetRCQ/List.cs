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

namespace Application.TUBEND.BeritaDetRCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdBerita { get; set; }
      public long? IdRek { get; set; }
      public decimal? Nilai { get; set; }
      public DateTime? DateCreate { get; set; }
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
        var parameters = new List<Expression<Func<BeritaDetR, bool>>>();

        if (request.IdBerita.HasValue)
          parameters.Add(d => d.IdBerita == request.IdBerita);

        if (request.IdRek.HasValue)
          parameters.Add(d => d.IdRek == request.IdRek);

        if (request.Nilai.HasValue)
          parameters.Add(d => d.Nilai == request.Nilai);

        if (request.DateCreate.HasValue)
          parameters.Add(d => d.DateCreate == request.DateCreate);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.BeritaDetR.FindAll(predicate).Count();

        var result = await _context.BeritaDetR
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdBeritaDet)
          .FindAllAsync<Berita, DaftRekening>(predicate, x => x.Berita,
            x => x.Rekening);

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