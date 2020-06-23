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

namespace Application.BUD.SP2DDetBCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdSP2D { get; set; }
      public long? IdRek { get; set; }
      public int? IdNoJeTra { get; set; }
      public decimal? Nilai { get; set; }
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
        var parameters = new List<Expression<Func<SP2DDetB, bool>>>();

        if (request.IdSP2D.HasValue)
          parameters.Add(d => d.IdSP2D == request.IdSP2D);

        if (request.IdRek.HasValue)
          parameters.Add(d => d.IdRek == request.IdRek);

        if (request.IdNoJeTra.HasValue)
          parameters.Add(d => d.IdNoJeTra == request.IdNoJeTra);

        if (request.Nilai.HasValue)
          parameters.Add(d => d.Nilai == request.Nilai);


        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.SP2DDetB.FindAll(predicate).Count();

        var result = await _context.SP2DDetB
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdSP2DDetB)
          .FindAllAsync<SP2D, DaftRekening>(
            predicate, x => x.SP2D,
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