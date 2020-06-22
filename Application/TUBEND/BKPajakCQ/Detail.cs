using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.BKPajakCQ
{
  public class Detail
  {
    public class Query : IRequest<BkPajak>
    {
      public long IdBkPajak { get; set; }
    }

    public class Handler : IRequestHandler<Query, BkPajak>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BkPajak> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.BkPajak.FindByIdAsync(request.IdBkPajak);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
