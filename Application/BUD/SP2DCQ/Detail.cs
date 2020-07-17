using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.MA;
using Domain.TUBEND;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BUD.SP2DCQ
{
  public class Detail
  {
    public class Query : IRequest<SP2DDTO>
    {
      public long IdSP2D { get; set; }
    }

    public class Handler : IRequestHandler<Query, SP2DDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SP2DDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.SP2D.FindAllAsync<DaftUnit, Bend, SPD, DaftPhk3, JabTtd, Kontrak>(
            x => x.IdSP2D == request.IdSP2D, x => x.Unit, x => x.Bend, x => x.SPD,
            x => x.Phk3, x => x.JabTtd, x => x.Kontrak)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<SP2DDTO>(result);
      }
    }
  }
}
