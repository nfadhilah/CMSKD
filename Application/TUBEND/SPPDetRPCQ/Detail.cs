using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.TUBEND;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.SPPDetRPCQ
{
  public class Detail
  {
    public class Query : IRequest<SPPDetRPDTO>
    {
      public long IdSPPDetRP { get; set; }
    }

    public class Handler : IRequestHandler<Query, SPPDetRPDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPPDetRPDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result = (await _context.SPPDetRP
          .FindAllAsync<SPPDetR, Pajak>(
            x => x.IdSPPDetRP == request.IdSPPDetRP, x => x.SPPDetR,
            x => x.Pajak)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<SPPDetRPDTO>(result);
      }
    }
  }
}
