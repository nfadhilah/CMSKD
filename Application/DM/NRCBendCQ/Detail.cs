using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;

namespace Application.DM.NRCBendCQ
{
  public class Detail
  {
    public class Query : IRequest<NrcBend>
    {
      public long IdNrcBend { get; set; }
    }

    public class Handler : IRequestHandler<Query, NrcBend>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<NrcBend> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.NrcBend.FindByIdAsync(request.IdNrcBend);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
