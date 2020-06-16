using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using AutoMapper;
using Domain.DM;
using MediatR;
using Persistence;

namespace Application.DM.Urusan
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
        var totalItemsCount = _context.KPA.FindAll(
            u => u.IdUnit == request.IdUnit)
          .Count();

        var result = await _context.KPA
          .SetLimit(request.Limit, request.Offset)
          .FindAllAsync<DaftUnit, Pegawai>(
            u => u.IdUnit == request.IdUnit,
            u => u.DaftUnit, u => u.Pegawai);

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