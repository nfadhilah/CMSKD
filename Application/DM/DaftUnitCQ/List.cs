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

namespace Application.DM.DaftUnitCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdUrus { get; set; }
      public string KdUnit { get; set; }
      public string NmUnit { get; set; }
      public int? KdLevel { get; set; }
      public string Type { get; set; }
      public string AkroUnit { get; set; }
      public string Alamat { get; set; }
      public string Telepon { get; set; }
      public int? StAktif { get; set; }
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
        var parameters = new List<Expression<Func<DaftUnit, bool>>>();

        if (request.IdUrus.HasValue)
          parameters.Add(x => x.IdUrus == request.IdUrus);

        if (!string.IsNullOrWhiteSpace(request.KdUnit))
          parameters.Add(x => x.KdUnit.Contains(request.KdUnit));

        if (!string.IsNullOrWhiteSpace(request.NmUnit))
          parameters.Add(x => x.NmUnit.Contains(request.NmUnit));

        if (!string.IsNullOrWhiteSpace(request.AkroUnit))
          parameters.Add(x => x.AkroUnit.Contains(request.AkroUnit));

        if (!string.IsNullOrWhiteSpace(request.Alamat))
          parameters.Add(x => x.AkroUnit.Contains(request.Alamat));

        if (!string.IsNullOrWhiteSpace(request.Telepon))
          parameters.Add(x => x.Telepon.Contains(request.Telepon));

        if (request.KdLevel.HasValue)
          parameters.Add(x => x.KdLevel == request.KdLevel);

        if (request.StAktif.HasValue)
          parameters.Add(x => x.StAktif == request.StAktif);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.DaftUnit.FindAll(predicate).Count();

        var result = await _context.DaftUnit
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.KdUnit)
          .FindAllAsync();

        return new PaginationWrapper(
          _mapper.Map<IEnumerable<DaftUnitDTO>>(result), new Pagination
          {
            CurrentPage = request.CurrentPage,
            PageSize = request.PageSize,
            TotalItemsCount = totalItemsCount
          });
      }
    }
  }
}