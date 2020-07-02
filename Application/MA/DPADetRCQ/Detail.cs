using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.MA;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MA.DPADetRCQ
{
  public class Detail
  {

    public class Query : IRequest<DPADetRDTO>
    {
      public long IdDPADetR { get; set; }
    }

    public class Handler : IRequestHandler<Query, DPADetRDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DPADetRDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.DPADetR.FindAllAsync<DPAR>(x => x.IdDPADetR == request.IdDPADetR, c => c.DPAR)).FirstOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<DPADetRDTO>(result);
      }
    }
  }
}
