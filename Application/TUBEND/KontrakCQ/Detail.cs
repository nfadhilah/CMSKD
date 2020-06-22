using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.KontrakCQ
{
  public class Detail
  {
    public class Query : IRequest<Kontrak>
    {
      public long IdKontrak { get; set; }
    }

    public class Handler : IRequestHandler<Query, Kontrak>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<Kontrak> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.Kontrak.FindByIdAsync(request.IdKontrak);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
