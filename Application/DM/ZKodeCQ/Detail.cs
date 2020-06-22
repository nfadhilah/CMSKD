using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.ZKodeCQ
{
  public class Detail
  {
    public class Query : IRequest<ZKode>
    {
      public int IdxKode { get; set; }
    }

    public class Handler : IRequestHandler<Query, ZKode>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<ZKode> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result = await _context.ZKode
          .FindByIdAsync(request.IdxKode);

        if (result == null) throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
