using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.BUD;
using Domain.DM;
using Domain.TUBEND;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BUD.BKUKCQ
{
  public class Detail
  {
    public class Query : IRequest<BKUK>
    {
      public long IdBKUK { get; set; }
    }

    public class Handler : IRequestHandler<Query, BKUK>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BKUK> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.BKUK.FindAllAsync<DaftUnit, STS>(
            x => x.IdBKUK == request.IdBKUK, x => x.Unit, x => x.SP2D)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
