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

namespace Application.DM.JUsahaCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public string BadanUsaha { get; set; }
      public string Keterangan { get; set; }
      public string Akronim { get; set; }
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
        var parameters = new List<Expression<Func<JUsaha, bool>>>();

        if (!string.IsNullOrWhiteSpace(request.BadanUsaha))
          parameters.Add(p => p.BadanUsaha.Contains(request.BadanUsaha));

        if (!string.IsNullOrWhiteSpace(request.Keterangan))
          parameters.Add(p => p.Keterangan.Contains(request.Keterangan));

        if (!string.IsNullOrWhiteSpace(request.Akronim))
          parameters.Add(p => p.Akronim.Contains(request.Akronim));

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.MPgrm.FindAll().Count();

        var result = await _context.JUsaha
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, x => x.BadanUsaha)
          .FindAllAsync(predicate);

        return new PaginationWrapper(result, new Pagination
        {
          CurrentPage = request.CurrentPage,
          PageSize = request.PageSize,
          TotalItemsCount = totalItemsCount
        });
      }
    }
  }
}
