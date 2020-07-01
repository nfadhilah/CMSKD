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

namespace Application.TUBEND.BkBankDetCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdBkBank { get; set; }
      public int? IdNoJeTra { get; set; }
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
        var parameters = new List<Expression<Func<BkBankDet, bool>>>();

        if (request.IdBkBank.HasValue)
          parameters.Add(d => d.IdBkBank == request.IdBkBank);

        if (request.IdNoJeTra.HasValue)
          parameters.Add(d => d.IdNoJeTra == request.IdNoJeTra);

        if (request.Nilai.HasValue)
          parameters.Add(d => d.Nilai == request.Nilai);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.BkBankDet.FindAll(predicate).Count();

        var result = await _context.BkBankDet
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdBankDet)
          .FindAllAsync<BkBank, JTrnlKas>(predicate, x => x.BkBank,
            x => x.JTrnlKas);

        return new PaginationWrapper(
          _mapper.Map<IEnumerable<BKBankDetDTO>>(result), new Pagination
          {
            CurrentPage = request.CurrentPage,
            PageSize = request.PageSize,
            TotalItemsCount = totalItemsCount
          });
      }
    }
  }
}