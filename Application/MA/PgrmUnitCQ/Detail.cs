using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MA.PgrmUnitCQ
{
  public class Detail
  {
    public class Query : IRequest<PgrmUnitDTO>
    {
      public long IdPgrmUnit { get; set; }
    }

    public class Handler : IRequestHandler<Query, PgrmUnitDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<PgrmUnitDTO> Handle(
        Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.PgrmUnit.FindAllAsync<DaftUnit, MPgrm>(
            x => x.IdPgrmUnit == request.IdPgrmUnit, c => c.DaftUnit,
            c => c.MPgrm)).FirstOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<PgrmUnitDTO>(result);
      }
    }
  }
}