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

namespace Application.TUBEND.BPKCQ
{
  public class Detail
  {
    public class Query : IRequest<BPKDTO>
    {
      public long IdBPK { get; set; }
    }

    public class Handler : IRequestHandler<Query, BPKDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BPKDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result = (await _context.BPK
          .FindAllAsync<DaftUnit, DaftPhk3, Bend, Berita, JBayar>(
            x => x.IdBPK == request.IdBPK, x => x.Unit,
            x => x.Phk3, x => x.Bend, x => x.Berita, x => x.JBayar)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<BPKDTO>(result);
      }
    }
  }
}
