using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.BUD;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BUD.DPDetCQ
{
  public class Detail
  {
    public class Query : IRequest<DPDetDTO>
    {
      public long IdDPDet { get; set; }
    }

    public class Handler : IRequestHandler<Query, DPDetDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DPDetDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.DPDet.FindAllAsync<SP2D>(
            x => x.IdDPDet == request.IdDPDet, x => x.SP2D)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<DPDetDTO>(result);
      }
    }
  }
}
