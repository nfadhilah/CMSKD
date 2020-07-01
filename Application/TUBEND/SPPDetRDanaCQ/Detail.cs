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

namespace Application.TUBEND.SPPDetRDanaCQ
{
  public class Detail
  {
    public class Query : IRequest<SPPDetRDanaDTO>
    {
      public long IdSPPDetRDana { get; set; }
    }

    public class Handler : IRequestHandler<Query, SPPDetRDanaDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPPDetRDanaDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result = (await _context.SPPDetRDana
          .FindAllAsync<SPPDetR, JDana>(
            x => x.IdSPPDetRDana == request.IdSPPDetRDana, x => x.SPPDetR,
            x => x.JDana)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<SPPDetRDanaDTO>(result);
      }
    }
  }
}
