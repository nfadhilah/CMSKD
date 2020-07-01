using Application.CommonDTO;
using Application.Helpers;
using AutoMapper;
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

namespace Application.TUBEND.SPPDetBDanaCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdSPPDetB { get; set; }
      public long? IdJDana { get; set; }
    }

    public class Handler : IRequestHandler<Query, PaginationWrapper>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<PaginationWrapper> Handle(
        Query request, CancellationToken cancellationToken)
      {
        var parameters = new List<Expression<Func<SPPDetBDana, bool>>>();

        if (request.IdSPPDetB.HasValue)
          parameters.Add(d => d.IdSPPDetB == request.IdSPPDetB);

        if (request.IdJDana.HasValue)
          parameters.Add(d => d.IdJDana == request.IdJDana);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.SPPDetBDana.FindAll(predicate).Count();

        var result = await _context.SPPDetBDana
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdSPPDetBDana)
          .FindAllAsync<SPPDetB, JDana>(
            predicate, x => x.SPPDetB,
            x => x.JDana);

        return new PaginationWrapper(
          _mapper.Map<IEnumerable<SPPDetBDanaDTO>>(result), new Pagination
          {
            CurrentPage = request.CurrentPage,
            PageSize = request.PageSize,
            TotalItemsCount = totalItemsCount
          });
      }
    }
  }
}