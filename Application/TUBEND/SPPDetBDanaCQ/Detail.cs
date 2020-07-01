using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.TUBEND;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.SPPDetBDanaCQ
{
  public class Detail
  {
    public class Query : IRequest<SPPDetBDanaDTO>
    {
      public long IdSPPDetBDana { get; set; }
    }

    public class Handler : IRequestHandler<Query, SPPDetBDanaDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPPDetBDanaDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result = (await _context.SPPDetBDana
          .FindAllAsync<SPPDetR, JDana>(
            x => x.IdSPPDetBDana == request.IdSPPDetBDana, x => x.SPPDetB,
            x => x.JDana)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<SPPDetBDanaDTO>(result);
      }
    }
  }
}
