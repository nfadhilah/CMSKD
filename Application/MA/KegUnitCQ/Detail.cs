using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.MA;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MA.KegUnitCQ
{
  public class Detail
  {

    public class Query : IRequest<KegUnit>
    {
      public long IdKegUnit { get; set; }
    }

    public class Handler : IRequestHandler<Query, KegUnit>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<KegUnit> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.KegUnit.FindAllAsync<MPgrm, MKegiatan>(x => x.IdKegUnit == request.IdKegUnit, c => c.MPgrm, c => c.MKegiatan)).First();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
