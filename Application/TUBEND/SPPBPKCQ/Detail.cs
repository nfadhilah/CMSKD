using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.SPPBPKCQ
{
  public class Detail
  {
    public class Query : IRequest<SPPBPKDTO>
    {
      public long IdSPPBPK { get; set; }
    }

    public class Handler : IRequestHandler<Query, SPPBPKDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPPBPKDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result = await
          _context.SPPBPK.FindAllAsync<SPP, BPK>(x => x.IdSPPBPK == request.IdSPPBPK, x => x.SPP,
            x => x.BPK);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<SPPBPKDTO>(result.Single());
      }
    }
  }
}
