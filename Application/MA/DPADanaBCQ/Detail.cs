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
    public class Query : IRequest<DPADanaBDTO>
    {
      public long IdDPADanaB { get; set; }
    }

    public class Handler : IRequestHandler<Query, DPADanaBDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DPADanaBDTO> Handle(
        Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.DPADanaB.FindAllAsync<DPAB, JDana>(
            x => x.IdDPADanaB == request.IdDPADanaB, c => c.DPAB, c => c.JDana))
          .FirstOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<DPADanaBDTO>(result);
      }
    }
  }
}