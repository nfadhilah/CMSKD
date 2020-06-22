using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.SPPDetBCQ
{
  public class Detail
  {
    public class Query : IRequest<SPPDetB>
    {
      public long IdSPPDetB { get; set; }
    }

    public class Handler : IRequestHandler<Query, SPPDetB>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPPDetB> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.SPPDetB.FindByIdAsync(request.IdSPPDetB);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
