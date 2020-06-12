using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Urusan
{
  public class Detail
  {

    public class Query : IRequest<UrusanUnit>
    {
      public long IdUnit { get; set; }
      public long UrusKey { get; set; }
    }

    public class Handler : IRequestHandler<Query, UrusanUnit>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<UrusanUnit> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var query = await _context.UrusanUnit
          .FindAllAsync<DaftUnit, DaftUnit>(
            u => u.IdUnit == request.IdUnit && u.UrusKey == request.UrusKey,
            u => u.DaftUnit, u => u.Urusan);

        var result = query.SingleOrDefault();

        if (result == null) throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
