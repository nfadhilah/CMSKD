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

namespace Application.TUBEND.BkPajakDetStrCQ
{
  public class Detail
  {
    public class Query : IRequest<BkPajakDetStrDTO>
    {
      public long IdBkPajakDetStr { get; set; }
    }

    public class Handler : IRequestHandler<Query, BkPajakDetStrDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BkPajakDetStrDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.BkPajakDetStr
            .FindAllAsync<BPKPajakStr, Pajak, BkPajak>(
              x => x.IdBkPajakDetStr == request.IdBkPajakDetStr,
              x => x.BPKPajakStr, x => x.Pajak, x => x.BkPajak)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<BkPajakDetStrDTO>(result);
      }
    }
  }
}
