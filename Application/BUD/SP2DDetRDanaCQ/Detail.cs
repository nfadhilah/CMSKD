using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.BUD;
using Domain.DM;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BUD.SP2DDetRDanaCQ
{
  public class Detail
  {
    public class Query : IRequest<SP2DDetRDana>
    {
      public long IdSP2DDetRDana { get; set; }
    }

    public class Handler : IRequestHandler<Query, SP2DDetRDana>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SP2DDetRDana> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.SP2DDetRDana.FindAllAsync<SP2DDetR, JDana>(
            x => x.IdSP2DDetRDana == request.IdSP2DDetRDana, x => x.SP2DDetR,
            x => x.JDana)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
