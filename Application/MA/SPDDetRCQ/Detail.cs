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

namespace Application.MA.SPDDetRCQ
{
  public class Detail
  {
    public class Query : IRequest<SPDDetRDTO>
    {
      public long IdSPDDetR { get; set; }
    }

    public class Handler : IRequestHandler<Query, SPDDetRDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPDDetRDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result = (await
          _context.SPDDetR.FindAllAsync<SPD, MKegiatan, DaftRekening>(
            x => x.IdSPDDetR == request.IdSPDDetR, x => x.SPD, x => x.Kegiatan,
            x => x.Rekening)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<SPDDetRDTO>(result);
      }
    }
  }
}
