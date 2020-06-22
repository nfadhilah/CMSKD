using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.SPPDetRCQ
{
  public class Detail
  {
    public class Query : IRequest<SPPDetR>
    {
      public long IdSPPDetR { get; set; }
    }

    public class Handler : IRequestHandler<Query, SPPDetR>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPPDetR> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.SPPDetR.FindByIdAsync(request.IdSPPDetR);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
