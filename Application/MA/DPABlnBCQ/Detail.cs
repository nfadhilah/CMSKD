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

namespace Application.MA.DPABlnBCQ
{
  public class Detail
  {

    public class Query : IRequest<DPABlnB>
    {
      public long IdDPABlnB { get; set; }
    }

    public class Handler : IRequestHandler<Query, DPABlnB>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DPABlnB> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.DPABlnB.FindAllAsync<DPAB>(x => x.IdDPABlnB == request.IdDPABlnB, c => c.DPAB)).First();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
