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

namespace Application.DM.DaftPhk3CQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdUnit { get; set; }
      public string NmPhk3 { get; set; }
      public string NmInst { get; set; }
      public int? IdJUsaha { get; set; }
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
        var parameters = new List<Expression<Func<DaftPhk3, bool>>>();

        if (request.IdUnit.HasValue)
          parameters.Add(d => d.IdUnit == request.IdUnit);

        if (!string.IsNullOrWhiteSpace(request.NmPhk3))
          parameters.Add(d => d.NmPhk3.Contains(request.NmPhk3));

        if (!string.IsNullOrWhiteSpace(request.NmInst))
          parameters.Add(d => d.NmInst.Contains(request.NmInst));

        if (request.IdJUsaha.HasValue)
          parameters.Add(d => d.IdJUsaha == request.IdJUsaha);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.DaftPhk3.FindAll(predicate).Count();

        var result = await _context.DaftPhk3
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.IdPhk3)
          .FindAllAsync<JBank, JUsaha>(predicate, x => x.Bank, x => x.JUsaha);

        return new PaginationWrapper(
          _mapper.Map<IEnumerable<DaftPhk3DTO>>(result), new Pagination
          {
            CurrentPage = request.CurrentPage,
            PageSize = request.PageSize,
            TotalItemsCount = totalItemsCount
          });
      }
    }
  }
}