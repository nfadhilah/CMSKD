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

namespace Application.DM.JabTtdCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdUnit { get; set; }
      public long? IdPeg { get; set; }
      public string KdDok { get; set; }
      public string Jabatan { get; set; }
      public string NoSKPTtd { get; set; }
      public DateTime? TglSKPTtd { get; set; }
      public string NoSKStopTtd { get; set; }
      public DateTime? TglSKStopTtd { get; set; }
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
        var parameters = new List<Expression<Func<JabTtd, bool>>>();

        if (request.IdUnit.HasValue)
          parameters.Add(d => d.IdUnit == request.IdUnit);

        if (request.IdPeg.HasValue)
          parameters.Add(d => d.IdPeg == request.IdPeg);

        if (!string.IsNullOrWhiteSpace(request.KdDok))
          parameters.Add(d => d.KdDok.Contains(request.KdDok));

        if (!string.IsNullOrWhiteSpace(request.Jabatan))
          parameters.Add(d => d.Jabatan.Contains(request.Jabatan));

        if (request.TglSKPTtd.HasValue)
          parameters.Add(d => d.TglSKPTtd == request.TglSKPTtd);

        if (!string.IsNullOrWhiteSpace(request.NoSKStopTtd))
          parameters.Add(d => d.NoSKStopTtd.Contains(request.NoSKStopTtd));

        if (request.TglSKStopTtd.HasValue)
          parameters.Add(d => d.TglSKStopTtd == request.TglSKStopTtd);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.JabTtd.FindAll(predicate).Count();

        var result = await _context.JabTtd
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdTtd)
          .FindAllAsync<DaftUnit, Pegawai>(predicate, x => x.DaftUnit,
            x => x.Pegawai);

        return new PaginationWrapper(
          _mapper.Map<IEnumerable<JabTTdDTO>>(result), new Pagination
          {
            CurrentPage = request.CurrentPage,
            PageSize = request.PageSize,
            TotalItemsCount = totalItemsCount
          });
      }
    }
  }
}