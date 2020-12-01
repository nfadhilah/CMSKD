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

namespace Application.TUBEND.TagihanDetCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdTagihan { get; set; }
      public long? IdRek { get; set; }
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
        var parameters = new List<Expression<Func<TagihanDet, bool>>>();

        if (request.IdTagihan.HasValue)
          parameters.Add(d => d.IdTagihan == request.IdTagihan);

        if (request.IdRek.HasValue)
          parameters.Add(d => d.IdRek == request.IdRek);

        if (request.Nilai.HasValue)
          parameters.Add(d => d.Nilai == request.Nilai);


        var predicate = PredicateBuilder.ComposeWithAnd(parameters);


        var result = await _context.TagihanDet
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdTagihanDet)
          .FindAllAsync<DaftRekening>(predicate, x => x.Rekening);

        return new PaginationWrapper(_mapper.Map<IEnumerable<TagihanDetDTO>>(result),
          new Pagination
          {
            CurrentPage = request.CurrentPage,
            PageSize = request.PageSize,
            TotalItemsCount = result.Count()
          });
      }
    }
  }
}