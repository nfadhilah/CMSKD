using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Helpers;
using AutoMapper;
using Domain.DM;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;

namespace Application.DM.Program
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long? IdUnit { get; set; }
      public string NmPrgrm { get; set; }
      public string NuPrgrm { get; set; }
      public string IdPrioda { get; set; }
      public string IdPrioProv { get; set; }
      public string IdPrioNas { get; set; }
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
        var parameters = new List<Expression<Func<MPgrm, bool>>>();

        if (request.IdUnit.HasValue)
          parameters.Add(p => p.IdUnit == request.IdUnit);

        if (!string.IsNullOrWhiteSpace(request.NmPrgrm))
          parameters.Add(p => p.NmPrgrm.Contains(request.NmPrgrm));

        if (!string.IsNullOrWhiteSpace(request.NuPrgrm))
          parameters.Add(p => p.NuPrgrm.Contains(request.NuPrgrm));

        if (!string.IsNullOrWhiteSpace(request.IdPrioda))
          parameters.Add(p => p.IdPrioda.Contains(request.IdPrioda));

        if (!string.IsNullOrWhiteSpace(request.IdPrioProv))
          parameters.Add(p => p.IdPrioProv.Contains(request.IdPrioProv));

        if (!string.IsNullOrWhiteSpace(request.IdPrioNas))
          parameters.Add(p => p.IdPrioNas.Contains(request.IdPrioNas));

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var totalItemsCount = _context.MPgrm.FindAll().Count();

        var result = await _context.MPgrm
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, x => x.NuPrgrm)
          .FindAllAsync<DaftUnit>(predicate, x => x.Urusan);

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
