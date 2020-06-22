using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.MA;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MA.SPDDetBCQ
{
  public class Detail
  {
    public class Query : IRequest<SPDDetB>
    {
      public long IdSPDDetB { get; set; }
    }

    public class Handler : IRequestHandler<Query, SPDDetB>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPDDetB> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.SPDDetB.FindByIdAsync(request.IdSPDDetB);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
