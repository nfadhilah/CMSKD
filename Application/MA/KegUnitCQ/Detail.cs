using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MA.KegUnitCQ
{
  public class Detail
  {
    public class Query : IRequest<KegUnitDTO>
    {
      public long IdKegUnit { get; set; }
    }

    public class Handler : IRequestHandler<Query, KegUnitDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<KegUnitDTO> Handle(
        Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.KegUnit
            .FindAllAsync<DaftUnit, MPgrm, MKegiatan, Pegawai>(
              x => x.IdKegUnit == request.IdKegUnit, c => c.Unit, c => c.MPgrm,
              c => c.MKegiatan, c => c.Pegawai)).FirstOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<KegUnitDTO>(result);
      }
    }
  }
}