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

namespace Application.MA.DPARCQ
{
  public class Detail
  {

    public class Query : IRequest<DPAR>
    {
      public long IdDPAR { get; set; }
    }

    public class Handler : IRequestHandler<Query, DPAR>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DPAR> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.DPAR.FindAllAsync<DPA, DaftRekening>(x => x.IdDPAR == request.IdDPAR, c => c.DPA, c => c.DaftRekening)).First();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
