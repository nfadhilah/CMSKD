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

namespace Application.MA.DPADetBCQ
{
  public class Detail
  {

    public class Query : IRequest<DPADetB>
    {
      public long IdDPADetB { get; set; }
    }

    public class Handler : IRequestHandler<Query, DPADetB>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DPADetB> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.DPADetB.FindAllAsync<DPAB>(x => x.IdDPADetB == request.IdDPADetB, c => c.DPAB)).First();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
