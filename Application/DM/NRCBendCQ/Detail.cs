using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.NrcBendCQ
{
  public class Detail
  {
    public class Query : IRequest<NrcBendDTO>
    {
      public long IdNrcBend { get; set; }
    }

    public class Handler : IRequestHandler<Query, NrcBendDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<NrcBendDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.NrcBend.FindAllAsync<DaftRekening>(
            x => x.IdNrcBend == request.IdNrcBend, x => x.DaftRekening)).Single();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<NrcBendDTO>(result);
      }
    }
  }
}
