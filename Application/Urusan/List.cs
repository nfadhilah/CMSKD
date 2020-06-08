using Application.Dtos;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Urusan
{
  public class List
  {
    public class Query : PaginationQuery, IRequest<PaginationWrapper>
    {
      public long IdUnit { get; set; }
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
        var totalItemsCount = _context.UrusanUnit.FindAll(
            u => u.IdUnit == request.IdUnit)
          .Count();

        var result = await _context.UrusanUnit
          .SetLimit(request.Limit, request.Offset)
          .FindAllAsync<DaftUnit, DaftUnit>(
            u => u.IdUnit == request.IdUnit,
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