using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.BPKCQ
{
  public class Detail
  {
    public class Query : IRequest<BPK>
    {
      public long IdBPK { get; set; }
    }

    public class Handler : IRequestHandler<Query, BPK>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BPK> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.BPK.FindByIdAsync(request.IdBPK);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
