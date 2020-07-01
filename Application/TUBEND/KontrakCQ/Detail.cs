using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.KontrakCQ
{
  public class Detail
  {
    public class Query : IRequest<KontrakDTO>
    {
      public long IdKontrak { get; set; }
    }

    public class Handler : IRequestHandler<Query, KontrakDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<KontrakDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result = (await _context.Kontrak
          .FindAllAsync<DaftUnit, DaftPhk3, MKegiatan>(
            x => x.IdKontrak == request.IdKontrak, x => x.Unit,
            x => x.Phk3, x => x.Kegiatan)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<KontrakDTO>(result);
      }
    }
  }
}
