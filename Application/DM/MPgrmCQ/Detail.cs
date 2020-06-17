using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;

namespace Application.DM.MPgrmCQ
{
  public class Detail
  {
    public class Query : IRequest<MPgrm>
    {
      public long IdPrgrm { get; set; }
    }

    public class Handler : IRequestHandler<Query, MPgrm>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<MPgrm> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.MPgrm.FindAsync(m => m.IdPrgrm == request.IdPrgrm);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
