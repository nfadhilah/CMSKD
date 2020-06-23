using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.MA;
using Domain.TUBEND;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.SPMCQ
{
  public class Detail
  {
    public class Query : IRequest<SPM>
    {
      public long IdSPM { get; set; }
    }

    public class Handler : IRequestHandler<Query, SPM>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPM> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.SPM.FindAllAsync<DaftUnit, Bend, SPD, SPP, DaftPhk3>(
            x => x.IdSPM == request.IdSPM, x => x.Unit, x => x.Bend, x => x.SPD,
            x => x.SPP, x => x.Phk3)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
