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

namespace Application.TUBEND.SPPDetRCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdRek { get; set; }
      public long? IdKeg { get; set; }
      public long? IdSPP { get; set; }
      public int? IdNoJeTra { get; set; }
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
        var parameters = new List<Expression<Func<SPPDetR, bool>>>();

        if (request.IdRek.HasValue)
          parameters.Add(d => d.IdRek == request.IdRek);

        if (request.IdKeg.HasValue)
          parameters.Add(d => d.IdKeg == request.IdKeg);

        if (request.IdSPP.HasValue)
          parameters.Add(d => d.IdSPP == request.IdSPP);

        if (request.IdNoJeTra.HasValue)
          parameters.Add(d => d.IdNoJeTra == request.IdNoJeTra);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.SPPDetR.FindAll(predicate).Count();

        var result = await _context.SPPDetR
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdSPPDetR)
          .FindAllAsync<DaftRekening, MKegiatan, SPP, JTrnlKas>(
            predicate, x => x.Rekening,
            x => x.Kegiatan, x => x.SPP, x => x.JTrnlKas);

        return new PaginationWrapper(
          _mapper.Map<IEnumerable<SPPDetRDTO>>(result), new Pagination
          {
            CurrentPage = request.CurrentPage,
            PageSize = request.PageSize,
            TotalItemsCount = totalItemsCount
          });
      }
    }
  }
}