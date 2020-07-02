using AutoMapper;
using AutoWrapper.Wrappers;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BUD.DPCQ
{
  public class Detail
  {
    public class Query : IRequest<DPDTO>
    {
      public long IdDP { get; set; }
    }

    public class Handler : IRequestHandler<Query, DPDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DPDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result = await _context.DP.FindByIdAsync(request.IdDP);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<DPDTO>(result);
      }
    }
  }
}
