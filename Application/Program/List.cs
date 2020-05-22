using Application.Dtos;
using AutoMapper;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Program
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
        var totalItemsCount = _context.MPgrm.FindAll().Count();

        var result = await _context.MPgrm
          .SetLimit(request.Limit, request.Offset)
          .SetOrderBy(OrderInfo.SortDirection.ASC, x => x.NuPrgrm)
          .FindAllAsync();

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
