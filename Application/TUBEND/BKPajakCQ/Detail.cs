using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.BKPajakCQ
{
  public class Detail
  {
    public class Query : IRequest<BkPajakDTO>
    {
      public long IdBkPajak { get; set; }
    }

    public class Handler : IRequestHandler<Query, BkPajakDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BkPajakDTO> Handle(
        Query request, CancellationToken cancellationToken)
      {
        var result = (await _context.BkPajak
            .FindAllAsync<DaftUnit, Bend>(
              x => x.IdBkPajak == request.IdBkPajak, x => x.Unit, x => x.Bend))
          .SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<BkPajakDTO>(result);
      }
    }
  }
}