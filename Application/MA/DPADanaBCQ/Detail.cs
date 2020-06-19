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

namespace Application.MA.DPADanaBCQ
{
  public class Detail
  {

    public class Query : IRequest<DPADanaB>
    {
      public long IdDPADanaB { get; set; }
    }

    public class Handler : IRequestHandler<Query, DPADanaB>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DPADanaB> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.DPADanaB.FindAllAsync<DPAB>(x => x.IdDPADanaB == request.IdDPADanaB, c => c.DPAB)).First();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
