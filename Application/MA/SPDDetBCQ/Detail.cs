using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.MA;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MA.SPDDetBCQ
{
  public class Detail
  {
    public class Query : IRequest<SPDDetBDTO>
    {
      public long IdSPDDetB { get; set; }
    }

    public class Handler : IRequestHandler<Query, SPDDetBDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPDDetBDTO> Handle(
        Query request, CancellationToken cancellationToken)
      {
        var result = (await _context.SPDDetB.FindAllAsync<SPD, DaftRekening>(
            x => x.IdSPDDetB == request.IdSPDDetB, x => x.SPD, x => x.Rekening))
          .SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<SPDDetBDTO>(result);
      }
    }
  }
}