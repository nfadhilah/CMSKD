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

namespace Application.DM.UrusanUnitCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdUnit { get; set; }
      public long? IdUrus { get; set; }
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
        var parameters = new List<Expression<Func<UrusanUnit, bool>>>();

        if (request.IdUrus.HasValue)
          parameters.Add(x => x.IdUrus == request.IdUrus);

        if (request.IdUnit.HasValue)
          parameters.Add(x => x.IdUnit == request.IdUnit);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.UrusanUnit.FindAll(
            predicate)
          .Count();

        var result = await _context.UrusanUnit
          .SetLimit(request.Limit, request.Offset)
          .FindAllAsync<DaftUnit, DaftUnit>(
            predicate,
            u => u.DaftUnit, u => u.Urusan);

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