using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.BUD;
using Domain.DM;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BUD.SP2DDetBCQ
{
  public class Detail
  {
    public class Query : IRequest<SP2DDetB>
    {
      public long IdSP2DDetB { get; set; }
    }

    public class Handler : IRequestHandler<Query, SP2DDetB>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SP2DDetB> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.SP2DDetB.FindAllAsync<SP2D, DaftRekening>(
            x => x.IdSP2D == request.IdSP2DDetB, x => x.SP2D,
            x => x.Rekening)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
