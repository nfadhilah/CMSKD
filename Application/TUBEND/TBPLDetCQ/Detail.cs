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

namespace Application.TUBEND.TBPLDetCQ
{
  public class Detail
  {
    public class Query : IRequest<TBPLDetDTO>
    {
      public long IdTBPLDet { get; set; }
    }

    public class Handler : IRequestHandler<Query, TBPLDetDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<TBPLDetDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result = (await _context.TBPLDet
          .FindAllAsync<TBPL, Bend, JTrnlKas>(
            x => x.IdTBPLDet == request.IdTBPLDet, x => x.TBPL,
            x => x.Bend, x => x.JTrnlKas)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<TBPLDetDTO>(result);
      }
    }
  }
}
