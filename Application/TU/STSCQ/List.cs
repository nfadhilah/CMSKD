using Application.Common.DTOS;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TU.STSCQ
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
    }

    public class Handler : IRequestHandler<Query, PaginationWrapper>
    {
      private readonly IDbContext _context;

      public Handler(IDbContext context)
      {
        _context = context;
      }

      public async Task<PaginationWrapper> Handle(Query request, CancellationToken cancellationToken)
      {
        var totalItems = await _context.STS.CountAsync();

        var result = await _context.STS.SetLimit(request.Limit, request.Offset)
          .FindAllAsync();

        return new PaginationWrapper
        {
          Data = result,
          Pagination = new Pagination
          {
            CurrentPage = request.CurrentPage,
            PageSize = request.PageSize,
            TotalItemsCount = totalItems
          }
        };
      }
    }
  }
}