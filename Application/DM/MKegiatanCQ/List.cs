using Application.CommonDTO;
using Application.Helpers;
using AutoMapper;
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

namespace Application.DM.MKegiatanCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdPrgrm { get; set; }
      public string KdPerspektif { get; set; }
      public string NuKeg { get; set; }
      public string NmKegUnit { get; set; }
      public int? LevelKeg { get; set; }
      public string Type { get; set; }
      public long? IdKegInduk { get; set; }
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
        var parameters = new List<Expression<Func<MKegiatan, bool>>>();

        if (request.IdPrgrm.HasValue)
          parameters.Add(p => p.IdPrgrm == request.IdPrgrm);

        if (!string.IsNullOrWhiteSpace(request.KdPerspektif))
          parameters.Add(p => p.KdPerspektif.Contains(request.KdPerspektif));

        if (!string.IsNullOrWhiteSpace(request.NuKeg))
          parameters.Add(p => p.NuKeg.Contains(request.NuKeg));

        if (!string.IsNullOrWhiteSpace(request.NmKegUnit))
          parameters.Add(p => p.NmKegUnit.Contains(request.NmKegUnit));

        if (request.LevelKeg.HasValue)
          parameters.Add(p => p.LevelKeg == request.LevelKeg);

        if (!string.IsNullOrWhiteSpace(request.Type))
          parameters.Add(p => p.Type.Contains(request.Type));

        if (request.IdKegInduk.HasValue)
          parameters.Add(p => p.IdKegInduk == request.IdKegInduk);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.MKegiatan.FindAll().Count();

        var result = await _context.MKegiatan
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, x => x.NuKeg)
          .FindAllAsync<MPgrm>(predicate, x => x.Program);

        return new PaginationWrapper(
          _mapper.Map<IEnumerable<MKegiatanDTO>>(result), new Pagination
          {
            CurrentPage = request.CurrentPage,
            PageSize = request.PageSize,
            TotalItemsCount = totalItemsCount
          });
      }
    }
  }
}