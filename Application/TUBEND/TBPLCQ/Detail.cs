using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.TBPLCQ
{
  public class Detail
  {
    public class Query : IRequest<TBPLDTO>
    {
      public long IdTBPL { get; set; }
    }

    public class Handler : IRequestHandler<Query, TBPLDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<TBPLDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result = (await _context.TBPL
          .FindAllAsync<DaftUnit, StatTrs, Bend, ZKode>(
            x => x.IdTBPL == request.IdTBPL, x => x.Unit,
            x => x.StatTrs, x => x.Bend, x => x.ZKode)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<TBPLDTO>(result);
      }
    }
  }
}
