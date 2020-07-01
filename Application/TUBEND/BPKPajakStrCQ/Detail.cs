using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.BPKPajakStrCQ
{
  public class Detail
  {
    public class Query : IRequest<BPKPajakStrDTO>
    {
      public long IdBkPajakStr { get; set; }
    }

    public class Handler : IRequestHandler<Query, BPKPajakStrDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BPKPajakStrDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.BPKPajakStr
            .FindAllAsync<BPKDetRP>(
              x => x.IdBkPajakStr == request.IdBkPajakStr,
              x => x.BPKDetRp)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<BPKPajakStrDTO>(result);
      }
    }
  }
}
