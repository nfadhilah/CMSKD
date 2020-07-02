using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.BendKPACQ
{
  public class Detail
  {
    public class Query : IRequest<BendKPADTO>
    {
      public long IdBendKPA { get; set; }
    }

    public class Handler : IRequestHandler<Query, BendKPADTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BendKPADTO> Handle(
        Query request, CancellationToken cancellationToken)
      {
        var result = (await _context.BendKPA.FindAllAsync<Bend, Pegawai>(
            x => x.IdBendKPA == request.IdBendKPA, x => x.Bend, x => x.Pegawai))
          .SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<BendKPADTO>(result);
      }
    }
  }
}