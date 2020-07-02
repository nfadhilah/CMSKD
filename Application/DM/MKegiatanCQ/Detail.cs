using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.MKegiatanCQ
{
  public class Detail
  {
    public class Query : IRequest<MKegiatanDTO>
    {
      public long IdKeg { get; set; }
    }

    public class Handler : IRequestHandler<Query, MKegiatanDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<MKegiatanDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result = (await
          _context.MKegiatan.FindAllAsync<MPgrm>(x => x.IdKeg == request.IdKeg,
            x => x.Program)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<MKegiatanDTO>(result);
      }
    }
  }
}
