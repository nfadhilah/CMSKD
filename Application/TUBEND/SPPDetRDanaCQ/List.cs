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

namespace Application.TUBEND.SPPDetRDanaCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdSPPDetR { get; set; }
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
        var parameters = new List<Expression<Func<SPPDetRDana, bool>>>();

        if (request.IdSPPDetR.HasValue)
          parameters.Add(d => d.IdSPPDetR == request.IdSPPDetR);

        if (request.IdJDana.HasValue)
          parameters.Add(d => d.IdJDana == request.IdJDana);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.SPPDetRDana.FindAll(predicate).Count();

        var result = await _context.SPPDetRDana
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdSPPDetRDana)
          .FindAllAsync<SPPDetR, JDana>(
            predicate, x => x.SPPDetR,
            x => x.JDana);

        return new PaginationWrapper(
          _mapper.Map<IEnumerable<SPPDetRDanaDTO>>(result), new Pagination
          {
            CurrentPage = request.CurrentPage,
            PageSize = request.PageSize,
            TotalItemsCount = totalItemsCount
          });
      }
    }
  }
}