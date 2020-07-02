using Application.CommonDTO;
using Application.Helpers;
using AutoMapper;
using Domain.BUD;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BUD.DPDetCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdDP { get; set; }
      public long? IdSP2D { get; set; }
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
        var parameters = new List<Expression<Func<DPDet, bool>>>();

        if (request.IdDP.HasValue)
          parameters.Add(d => d.IdDP == request.IdDP);

        if (request.IdSP2D.HasValue)
          parameters.Add(d => d.IdSP2D == request.IdSP2D);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.DPDet.FindAll(predicate).Count();

        var result = await _context.DPDet
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdDPDet)
          .FindAllAsync<SP2D>(predicate, x => x.SP2D);

        return new PaginationWrapper(_mapper.Map<IEnumerable<DPDetDTO>>(result),
          new Pagination
          {
            CurrentPage = request.CurrentPage,
            PageSize = request.PageSize,
            TotalItemsCount = totalItemsCount
          });
      }
    }
  }
}