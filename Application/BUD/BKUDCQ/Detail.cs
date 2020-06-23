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

namespace Application.BUD.BKUDCQ
{
  public class Detail
  {
    public class Query : IRequest<BKUD>
    {
      public long IdBKUD { get; set; }
    }

    public class Handler : IRequestHandler<Query, BKUD>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BKUD> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.BKUD.FindAllAsync<DaftUnit, STS>(
            x => x.IdBKUD == request.IdBKUD, x => x.Unit, x => x.STS)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
