using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.JenisBuktiKas
{
  public class Detail
  {

    public class Query : IRequest<JBKas>
    {
      public long IdBKas { get; set; }
    }

    public class Handler : IRequestHandler<Query, JBKas>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JBKas> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.JBKas.FindByIdAsync(request.IdBKas);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
