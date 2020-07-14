using Application.CommonDTO;
using Application.Helpers;
using AutoMapper;
using Domain.TUBEND;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.SPPBPKCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdSPP { get; set; }
      public long? IdBPK { get; set; }
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
        var parameters = new List<Expression<Func<SPPBPK, bool>>>();

        if (request.IdSPP.HasValue)
          parameters.Add(s => s.IdSPP == request.IdSPP);

        if (request.IdBPK.HasValue)
          parameters.Add(s => s.IdBPK == request.IdBPK);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var result = await
          _context.SPPBPK.FindAllAsync<SPP, BPK>(predicate, x => x.SPP,
            x => x.BPK);

        return new PaginationWrapper(
          _mapper.Map<IEnumerable<SPPBPKDTO>>(result), new Pagination
          {
            CurrentPage = request.CurrentPage,
            PageSize = request.PageSize,
            TotalItemsCount = result.Count()
          });
      }
    }
  }
}