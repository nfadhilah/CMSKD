using Application.CommonDTO;
using Application.Helpers;
using AutoMapper;
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

namespace Application.BUD.SP2DDetRDanaCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdSP2DDetRDana { get; set; }
      public long? IdSP2DDetR { get; set; }
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
        var parameters = new List<Expression<Func<SP2DDetRDana, bool>>>();

        if (request.IdSP2DDetRDana.HasValue)
          parameters.Add(d => d.IdSP2DDetRDana == request.IdSP2DDetRDana);

        if (request.IdSP2DDetR.HasValue)
          parameters.Add(d => d.IdSP2DDetR == request.IdSP2DDetR);

        if (request.IdJDana.HasValue)
          parameters.Add(d => d.IdJDana == request.IdJDana);

        if (request.Nilai.HasValue)
          parameters.Add(d => d.Nilai == request.Nilai);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.SP2DDetRDana.FindAll(predicate).Count();

        var result = await _context.SP2DDetRDana
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdSP2DDetRDana)
          .FindAllAsync<SP2DDetR, JDana>(
            predicate, x => x.SP2DDetR, x => x.JDana);

        return new PaginationWrapper(
          _mapper.Map<IEnumerable<SP2DDetRDanaDTO>>(result), new Pagination
          {
            CurrentPage = request.CurrentPage,
            PageSize = request.PageSize,
            TotalItemsCount = totalItemsCount
          });
      }
    }
  }
}