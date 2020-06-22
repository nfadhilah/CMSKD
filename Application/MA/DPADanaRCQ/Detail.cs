using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.MA;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MA.DPADanaRCQ
{
  public class Detail
  {

    public class Query : IRequest<DPADanaR>
    {
      public long IdDPADanaR { get; set; }
    }

    public class Handler : IRequestHandler<Query, DPADanaR>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DPADanaR> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.DPADanaR.FindAllAsync<DPAR>(x => x.IdDPADanaR == request.IdDPADanaR, c => c.DPAR)).First();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
