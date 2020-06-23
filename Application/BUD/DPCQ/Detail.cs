using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.BUD;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BUD.DPCQ
{
  public class Detail
  {
    public class Query : IRequest<DP>
    {
      public long IdDP { get; set; }
    }

    public class Handler : IRequestHandler<Query, DP>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DP> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result = await _context.DP.FindByIdAsync(request.IdDP);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
