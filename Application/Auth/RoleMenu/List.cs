using AutoMapper;
using Domain.Auth;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.CommonDTO;

namespace Application.Auth.RoleMenu
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
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
        var totalItemsCount = _context.WebOtor.FindAll().Count();

        var result = await _context.WebOtor
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, d => d.RoleId)
          .FindAllAsync<WebRole, WebGroup>(null, x => x.WebRole, x => x.WebGroup);

        return new PaginationWrapper(_mapper.Map<IEnumerable<WebOtorDto>>(result),
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