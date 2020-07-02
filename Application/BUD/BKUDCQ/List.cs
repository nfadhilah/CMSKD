using Application.CommonDTO;
using Application.Helpers;
using AutoMapper;
using Domain.BUD;
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

namespace Application.BUD.BKUDCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdKas { get; set; }
      public long? IdSTS { get; set; }
      public long? IdBKas { get; set; }
      public long? IdUnit { get; set; }
      public DateTime? TglKas { get; set; }
      public DateTime? TglValid { get; set; }
      public string Uraian { get; set; }
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
        var parameters = new List<Expression<Func<BKUD, bool>>>();

        if (request.IdUnit.HasValue)
          parameters.Add(d => d.IdUnit == request.IdUnit);

        if (!string.IsNullOrWhiteSpace(request.Uraian))
          parameters.Add(d => d.Uraian.Contains(request.Uraian));

        if (request.IdKas.HasValue)
          parameters.Add(d => d.IdKas == request.IdKas);

        if (request.IdSTS.HasValue)
          parameters.Add(d => d.IdSTS == request.IdSTS);

        if (request.IdBKas.HasValue)
          parameters.Add(d => d.IdBKas == request.IdBKas);

        if (request.TglKas.HasValue)
          parameters.Add(d => d.TglKas == request.TglKas);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.BKUD.FindAll(predicate).Count();

        var result = await _context.BKUD
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdBKUD)
          .FindAllAsync<DaftUnit, STS>(
            predicate, x => x.Unit, x => x.STS);

        return new PaginationWrapper(_mapper.Map<IEnumerable<BKUDDTO>>(result),
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