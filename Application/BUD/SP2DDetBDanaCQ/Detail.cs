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

namespace Application.BUD.SP2DDetBDanaCQ
{
  public class Detail
  {
    public class Query : IRequest<SP2DDetBDanaDTO>
    {
      public long IdSP2DDetBDana { get; set; }
    }

    public class Handler : IRequestHandler<Query, SP2DDetBDanaDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SP2DDetBDanaDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.SP2DDetBDana.FindAllAsync<SP2DDetR, JDana>(
            x => x.IdSP2DDetBDana == request.IdSP2DDetBDana, x => x.SP2DDetB,
            x => x.JDana)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<SP2DDetBDanaDTO>(result);
      }
    }
  }
}
