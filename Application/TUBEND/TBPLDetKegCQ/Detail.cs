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

namespace Application.TUBEND.TBPLDetKegCQ
{
  public class Detail
  {
    public class Query : IRequest<TBPLDetKegDTO>
    {
      public long IdTBPLDetKeg { get; set; }
    }

    public class Handler : IRequestHandler<Query, TBPLDetKegDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<TBPLDetKegDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result = (await _context.TBPLDetKeg
          .FindAllAsync<TBPLDet, JTrnlKas, MKegiatan>(
            x => x.IdTBPLDet == request.IdTBPLDetKeg, x => x.TBPLDet,
            x => x.JTrnlKas, x => x.Kegiatan)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<TBPLDetKegDTO>(result);
      }
    }
  }
}
