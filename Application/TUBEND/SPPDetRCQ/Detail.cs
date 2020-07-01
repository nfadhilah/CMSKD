using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.TUBEND;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.SPPDetRCQ
{
  public class Detail
  {
    public class Query : IRequest<SPPDetRDTO>
    {
      public long IdSPPDetR { get; set; }
    }

    public class Handler : IRequestHandler<Query, SPPDetRDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPPDetRDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result = (await _context.SPPDetR
          .FindAllAsync<DaftRekening, MKegiatan, SPP, JTrnlKas>(
            x => x.IdSPPDetR == request.IdSPPDetR, x => x.Rekening,
            x => x.Kegiatan, x => x.SPP, x => x.JTrnlKas)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<SPPDetRDTO>(result);
      }
    }
  }
}
