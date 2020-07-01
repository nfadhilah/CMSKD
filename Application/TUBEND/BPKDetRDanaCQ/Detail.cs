using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.TUBEND;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.BPKDetRDanaCQ
{
  public class Detail
  {
    public class Query : IRequest<BPKDetRDanaDTO>
    {
      public long IdBPKDetRDana { get; set; }
    }

    public class Handler : IRequestHandler<Query, BPKDetRDanaDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BPKDetRDanaDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result = (await _context.BPKDetRDana
          .FindAllAsync<BPKDetR, JDana>(
            x => x.IdBPKDetRDana == request.IdBPKDetRDana, x => x.BPKDetR,
            x => x.JDana));

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<BPKDetRDanaDTO>(result);
      }
    }
  }
}
