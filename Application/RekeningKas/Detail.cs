using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.RekeningKas
{
  public class Detail
  {

    public class Query : IRequest<BkBKas>
    {
      public long IdKas { get; set; }
    }

    public class Handler : IRequestHandler<Query, BkBKas>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BkBKas> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.BkBKas.FindAllAsync<DaftUnit, DaftRekening>(x => x.IdKas == request.IdKas, c => c.DaftUnit, c => c.DaftRekening)).First();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
