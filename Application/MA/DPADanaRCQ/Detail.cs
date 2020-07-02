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
    public class Query : IRequest<DPADanaRDTO>
    {
      public long IdDPADanaR { get; set; }
    }

    public class Handler : IRequestHandler<Query, DPADanaRDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DPADanaRDTO> Handle(
        Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.DPADanaR.FindAllAsync<DPAR, JDana>(
            x => x.IdDPADanaR == request.IdDPADanaR, c => c.DPAR, c => c.JDana))
          .FirstOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<DPADanaRDTO>(result);
      }
    }
  }
}