using Application.CommonDTO;
using Application.Helpers;
using AutoMapper;
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

namespace Application.TUBEND.TagihanCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdUnit { get; set; }
      public long? IdKeg { get; set; }
      public string NoTagihan { get; set; }
      public DateTime? TglTagihan { get; set; }
      public long? IdKontrak { get; set; }
      public string UraianTagihan { get; set; }
      public DateTime? TglValid { get; set; }
      public string KdStatus { get; set; }
      public DateTime? DateCreate { get; set; }
      public DateTime? DateUpdate { get; set; }
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
        var parameters = new List<Expression<Func<Tagihan, bool>>>();

        if (request.IdUnit.HasValue)
          parameters.Add(d => d.IdUnit == request.IdUnit);

        if (request.IdKeg.HasValue)
          parameters.Add(d => d.IdKeg == request.IdKeg);

        if (!string.IsNullOrWhiteSpace(request.NoTagihan))
          parameters.Add(x => x.NoTagihan == request.NoTagihan);

        if (request.TglTagihan.HasValue)
          parameters.Add(d => d.TglTagihan == request.TglTagihan);

        if (request.IdKontrak.HasValue)
          parameters.Add(d => d.IdKontrak == request.IdKontrak);

        if (!string.IsNullOrWhiteSpace(request.UraianTagihan))
          parameters.Add(x => x.UraianTagihan.Contains(request.UraianTagihan));

        if (request.TglValid.HasValue)
          parameters.Add(d => d.TglValid == request.TglValid);

        if (!string.IsNullOrWhiteSpace(request.KdStatus))
          parameters.Add(x => x.KdStatus == request.KdStatus);


        var predicate = PredicateBuilder.ComposeWithAnd(parameters);


        var result = await _context.Tagihan
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.NoTagihan)
          .FindAllAsync<Kontrak>(predicate, x => x.Kontrak);

        return new PaginationWrapper(_mapper.Map<IEnumerable<TagihanDTO>>(result),
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