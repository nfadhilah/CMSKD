using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.CommonDTO;
using Application.Helpers;
using AutoMapper;
using Domain.DM;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;

namespace Application.DM.ProvinsiCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public string IdProv { get; set; }
      public string Nama { get; set; }
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
        var parameters = new List<Expression<Func<Provinsi, bool>>>();

        if (!string.IsNullOrWhiteSpace(request.IdProv))
          parameters.Add(x => x.IdProv == request.IdProv);

        if (!string.IsNullOrWhiteSpace(request.Nama))
          parameters.Add(x => x.Nama == request.Nama);

        var predicate = PredicateBuilder.ComposeWithAnd(parameters);

        var result = await _context.Provinsi
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, x => x.IdProv)
          .FindAllAsync(predicate);

        return new PaginationWrapper(result, new Pagination
        {
          CurrentPage = request.CurrentPage,
          PageSize = request.PageSize,
          TotalItemsCount = result.Count()
        });
      }
    }
  }
}