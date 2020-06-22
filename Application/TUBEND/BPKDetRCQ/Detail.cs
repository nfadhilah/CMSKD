using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.BPKDetRCQ
{
  public class Detail
  {
    public class Query : IRequest<BPKDetR>
    {
      public long IdBPKDetR { get; set; }
    }

    public class Handler : IRequestHandler<Query, BPKDetR>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BPKDetR> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.BPKDetR.FindByIdAsync(request.IdBPKDetR);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
