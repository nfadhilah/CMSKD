using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.BendCQ
{
  public class Detail
  {

    public class Query : IRequest<BendDTO>
    {
      public long IdBend { get; set; }
    }

    public class Handler : IRequestHandler<Query, BendDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BendDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
      (await _context.Bend.FindAllAsync<Pegawai, JBank>(
        x => x.IdBend == request.IdBend, c => c.Peg, c => c.Bank)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<BendDTO>(result);
      }
    }
  }
}
