using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.MA;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MA.DPABlnRCQ
{
  public class Detail
  {

    public class Query : IRequest<DPABlnRDTO>
    {
      public long IdDPABlnR { get; set; }
    }

    public class Handler : IRequestHandler<Query, DPABlnRDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DPABlnRDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.DPABlnR.FindAllAsync<DPAR>(
            x => x.IdDPABlnR == request.IdDPABlnR, c => c.DPAR))
          .FirstOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<DPABlnRDTO>(result);
      }
    }
  }
}
