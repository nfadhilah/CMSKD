using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.JenisBuktiMemorial
{
  public class Detail
  {

    public class Query : IRequest<JBM>
    {
      public long IdJBM { get; set; }
    }

    public class Handler : IRequestHandler<Query, JBM>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JBM> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.JBM.FindAsync(x => x.IdJBM == request.IdJBM);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
