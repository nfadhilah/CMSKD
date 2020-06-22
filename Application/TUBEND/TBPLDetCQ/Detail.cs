using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.TBPLDetCQ
{
  public class Detail
  {
    public class Query : IRequest<TBPLDet>
    {
      public long IdTBPLDet { get; set; }
    }

    public class Handler : IRequestHandler<Query, TBPLDet>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<TBPLDet> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.TBPLDet.FindByIdAsync(request.IdTBPLDet);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
