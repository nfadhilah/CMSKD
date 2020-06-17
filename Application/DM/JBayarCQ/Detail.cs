using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.JBayarCQ
{
  public class Detail
  {

    public class Query : IRequest<JBayar>
    {
      public long IdJBayar { get; set; }
    }

    public class Handler : IRequestHandler<Query, JBayar>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JBayar> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.JBayar.FindAsync(x => x.IdJBayar == request.IdJBayar);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
