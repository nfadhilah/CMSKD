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

namespace Application.TUBEND.BPKDetRDanaCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdBPKDetR { get; set; }
      public long? IdJDana { get; set; }
      public decimal? Nilai { get; set; }
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
        var parameters = new List<Expression<Func<BPKDetRDana, bool>>>();

        if (request.IdBPKDetR.HasValue)
          parameters.Add(d => d.IdBPKDetR == request.IdBPKDetR);

        if (request.IdJDana.HasValue)
          parameters.Add(d => d.IdJDana == request.IdJDana);

        if (request.Nilai.HasValue)
          parameters.Add(d => d.Nilai == request.Nilai);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.BPKDetRDana.FindAll(predicate).Count();

        var result = await _context.BPKDetRDana
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdBPKDetRDana)
          .FindAllAsync<BPKDetR, JDana>(
            predicate, x => x.BPKDetR, x => x.JDana);

        return new PaginationWrapper(
          _mapper.Map<IEnumerable<BPKDetRDanaDTO>>(result), new Pagination
          {
            CurrentPage = request.CurrentPage,
            PageSize = request.PageSize,
            TotalItemsCount = totalItemsCount
          });
      }
    }
  }
}