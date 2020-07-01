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

namespace Application.TUBEND.BPKDetRCQ
{
  public class Detail
  {
    public class Query : IRequest<BPKDetRDTO>
    {
      public long IdBPKDetR { get; set; }
    }

    public class Handler : IRequestHandler<Query, BPKDetRDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BPKDetRDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result = (await _context.BPKDetR
          .FindAllAsync<BPK, MKegiatan, DaftRekening>(
            x => x.IdBPKDetR == request.IdBPKDetR, x => x.BPK,
            x => x.Kegiatan, x => x.Rekening)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<BPKDetRDTO>(result);
      }
    }
  }
}
