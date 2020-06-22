using Application.Dtos;
using Application.Helpers;
using AutoMapper;
using Domain.DM;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.ZKodeCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
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
        var parameters = new List<Expression<Func<ZKode, bool>>>();

        if (!string.IsNullOrEmpty(request.Uraian))
          parameters.Add(x => x.Uraian.Contains(request.Uraian));

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.ZKode.FindAll(
            predicate)
          .Count();

        var result = await _context.ZKode
          .SetLimit(request.Limit, request.Offset)
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