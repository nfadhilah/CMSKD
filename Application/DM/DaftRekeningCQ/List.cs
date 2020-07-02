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

namespace Application.DM.DaftRekeningCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public string KdPer { get; set; }
      public string NmPer { get; set; }
      public int? MtgLevel { get; set; }
      public int? KdKhusus { get; set; }
      public long? IdJRek { get; set; }
      public long? IdJnsAkun { get; set; }
      public string Type { get; set; }
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
        var parameters = new List<Expression<Func<DaftRekening, bool>>>();

        if (!string.IsNullOrWhiteSpace(request.KdPer))
          parameters.Add(d => d.KdPer.Contains(request.KdPer));

        if (!string.IsNullOrWhiteSpace(request.NmPer))
          parameters.Add(d => d.NmPer.Contains(request.NmPer));

        if (request.MtgLevel.HasValue)
          parameters.Add(d => d.MtgLevel == request.MtgLevel);

        if (request.KdKhusus.HasValue)
          parameters.Add(d => d.KdKhusus == request.KdKhusus);

        if (request.IdJRek.HasValue)
          parameters.Add(d => d.JnsRek == request.IdJRek);

        if (request.IdJnsAkun.HasValue)
          parameters.Add(d => d.IdJnsAkun == request.IdJnsAkun);

        if (!string.IsNullOrWhiteSpace(request.Type))
          parameters.Add(d => d.Type.Contains(request.Type));

        if (request.StAktif.HasValue)
          parameters.Add(d => d.StAktif == request.StAktif);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.DaftRekening.FindAll(predicate).Count();

        var result = await _context.DaftRekening
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.KdPer)
          .FindAllAsync(predicate);

        return new PaginationWrapper(
          _mapper.Map<IEnumerable<DaftRekeningDTO>>(result), new Pagination
          {
            CurrentPage = request.CurrentPage,
            PageSize = request.PageSize,
            TotalItemsCount = totalItemsCount
          });
      }
    }
  }
}