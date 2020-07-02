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

namespace Application.TUBEND.STSCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdUnit { get; set; }
      public string NoSTS { get; set; }
      public long? IdBend { get; set; }
      public string KdStatus { get; set; }
      public int? IdxKode { get; set; }
      public long? IdKas { get; set; }
      public DateTime? TglSTS { get; set; }
      public string Uraian { get; set; }
      public DateTime? TglValid { get; set; }
      public long? KdRilis { get; set; }
      public int? StKirim { get; set; }
      public int? StCair { get; set; }
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
        var parameters = new List<Expression<Func<STS, bool>>>();

        if (request.IdUnit.HasValue)
          parameters.Add(d => d.IdUnit == request.IdUnit);

        if (!string.IsNullOrWhiteSpace(request.NoSTS))
          parameters.Add(x => x.NoSTS.Contains(request.NoSTS));

        if (!string.IsNullOrWhiteSpace(request.Uraian))
          parameters.Add(x => x.Uraian.Contains(request.Uraian));

        if (request.IdBend.HasValue)
          parameters.Add(d => d.IdBend == request.IdBend);

        if (!string.IsNullOrWhiteSpace(request.KdStatus))
          parameters.Add(x => x.KdStatus == request.KdStatus);

        if (request.IdxKode.HasValue)
          parameters.Add(d => d.IdxKode == request.IdxKode);

        if (request.IdKas.HasValue)
          parameters.Add(d => d.IdKas == request.IdKas);

        if (request.TglSTS.HasValue)
          parameters.Add(d => d.TglSTS == request.TglSTS);

        if (request.TglValid.HasValue)
          parameters.Add(d => d.TglValid == request.TglValid);

        if (request.KdRilis.HasValue)
          parameters.Add(d => d.KdRilis == request.KdRilis);

        if (request.StKirim.HasValue)
          parameters.Add(d => d.StKirim == request.StKirim);

        if (request.StCair.HasValue)
          parameters.Add(d => d.StCair == request.StCair);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.STS.FindAll(predicate).Count();

        var result = await _context.STS
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.NoSTS)
          .FindAllAsync<DaftUnit, Bend>(
            predicate,
            x => x.Unit, x => x.Bend);

        return new PaginationWrapper(_mapper.Map<IEnumerable<STSDTO>>(result),
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