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

namespace Application.TUBEND.BPKDetRPCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdBPKDetR { get; set; }
      public long? IdPajak { get; set; }
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
        var parameters = new List<Expression<Func<BPKDetRP, bool>>>();

        if (request.IdBPKDetR.HasValue)
          parameters.Add(d => d.IdBPKDetR == request.IdBPKDetR);

        if (request.IdPajak.HasValue)
          parameters.Add(d => d.IdPajak == request.IdPajak);

        if (request.Nilai.HasValue)
          parameters.Add(d => d.Nilai == request.Nilai);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.BPKDetRP.FindAll(predicate).Count();

        var result = await _context.BPKDetRP
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdBPKDetRP)
          .FindAllAsync<BPKDetR, Pajak>(
            predicate, x => x.BPKDetR,
            x => x.Pajak);

        return new PaginationWrapper(
          _mapper.Map<IEnumerable<BPKDetRPDTO>>(result), new Pagination
          {
            CurrentPage = request.CurrentPage,
            PageSize = request.PageSize,
            TotalItemsCount = totalItemsCount
          });
      }
    }
  }
}