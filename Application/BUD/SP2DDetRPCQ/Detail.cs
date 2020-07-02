using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.BUD;
using Domain.DM;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BUD.SP2DDetRPCQ
{
  public class Detail
  {
    public class Query : IRequest<SP2DDetRPDTO>
    {
      public long IdSP2DDetRP { get; set; }
    }

    public class Handler : IRequestHandler<Query, SP2DDetRPDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SP2DDetRPDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.SP2DDetRP.FindAllAsync<SP2DDetR, Pajak>(
            x => x.IdSP2DDetRP == request.IdSP2DDetRP, x => x.SP2DDetR,
            x => x.Pajak)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<SP2DDetRPDTO>(result);
      }
    }
  }
}
