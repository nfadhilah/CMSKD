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
using Application.CommonDTO;

namespace Application.MA.SPDDetRCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdSPD { get; set; }
      public long? IdKeg { get; set; }
      public long? IdRek { get; set; }
      public decimal? Nilai { get; set; }
      public DateTime? DateCreate { get; set; }

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
          var parameters = new List<Expression<Func<SPDDetR, bool>>>();

          if (request.IdSPD.HasValue)
            parameters.Add(d => d.IdSPD == request.IdSPD);

          if (request.IdKeg.HasValue)
            parameters.Add(d => d.IdKeg == request.IdKeg);

          if (request.IdRek.HasValue)
            parameters.Add(d => d.IdRek == request.IdRek);

          if (request.Nilai.HasValue)
            parameters.Add(d => d.Nilai == request.Nilai);

          var predicate = PredicateBuilder.ComposeWithAnd(parameters);

          var totalItemsCount = _context.SPDDetR.FindAll(predicate).Count();

          var result = await _context.SPDDetR
            .SetLimit(request.Limit, request.Offset)
            .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdSPDDetR)
            .FindAllAsync<SPD, MKegiatan, DaftRekening>(predicate, x => x.SPD,
              x => x.Kegiatan, x => x.Rekening);

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
}