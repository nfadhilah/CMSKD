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

namespace Application.TUBEND.SPPCQ
{
  public class Detail
  {
    public class Query : IRequest<SPPDTO>
    {
      public long IdSPP { get; set; }
    }

    public class Handler : IRequestHandler<Query, SPPDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPPDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result = await _context.SPP
          .FindAllAsync<DaftUnit, StatTrs, Bend, SPD, DaftPhk3, Kontrak>(
            x => x.IdSPP == request.IdSPP, x => x.Unit,
            x => x.StatTrs, x => x.Bendahara, x => x.SPD, x => x.Phk3,
            x => x.Kontrak);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<SPPDTO>(result.SingleOrDefault());
      }
    }
  }
}
