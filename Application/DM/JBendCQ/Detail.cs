using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.JBendCQ
{
  public class Detail
  {
    public class Query : IRequest<JBend>
    {
      public string JnsBend { get; set; }
    }

    public class Handler : IRequestHandler<Query, JBend>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JBend> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.JBend.FindAsync(x => x.JnsBend == request.JnsBend);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
