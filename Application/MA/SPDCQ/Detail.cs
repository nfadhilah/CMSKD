using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MA.SPDCQ
{
  public class Detail
  {
    public class Query : IRequest<SPDDTO>
    {
      public long IdSPD { get; set; }
    }

    public class Handler : IRequestHandler<Query, SPDDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPDDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.SPD.FindAllAsync<DaftUnit>(x => x.IdSPD == request.IdSPD,
            x => x.Unit)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<SPDDTO>(result);
      }
    }
  }
}
