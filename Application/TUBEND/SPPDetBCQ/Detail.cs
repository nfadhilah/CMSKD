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

namespace Application.TUBEND.SPPDetBCQ
{
  public class Detail
  {
    public class Query : IRequest<SPPDetBDTO>
    {
      public long IdSPPDetB { get; set; }
    }

    public class Handler : IRequestHandler<Query, SPPDetBDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPPDetBDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result = (await _context.SPPDetB
          .FindAllAsync<DaftRekening, SPP, JTrnlKas>(
            x => x.IdSPPDetB == request.IdSPPDetB, x => x.Rekening, x => x.SPP,
            x => x.JTrnlKas)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<SPPDetBDTO>(result);
      }
    }
  }
}
