using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.JUsahaCQ
{
  public class Detail
  {
    public class Query : IRequest<JUsaha>
    {
      public long IdJUsaha { get; set; }
    }

    public class Handler : IRequestHandler<Query, JUsaha>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JUsaha> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.JUsaha.FindAsync(m => m.IdJUsaha == request.IdJUsaha);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
