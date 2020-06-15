using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.JabTandaTangan
{
  public class Detail
  {
    public class Query : IRequest<JabTtd>
    {
      public long IdTtd { get; set; }
    }

    public class Handler : IRequestHandler<Query, JabTtd>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JabTtd> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.JabTtd.FindByIdAsync(request.IdTtd);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
