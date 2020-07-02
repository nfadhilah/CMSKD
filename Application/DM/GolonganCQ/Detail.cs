using AutoMapper;
using AutoWrapper.Wrappers;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.GolonganCQ
{
  public class Detail
  {
    public class Query : IRequest<GolonganDTO>
    {
      public long IdGol { get; set; }
    }

    public class Handler : IRequestHandler<Query, GolonganDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<GolonganDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
         await _context.Golongan.FindAsync(x => x.IdGol == request.IdGol);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<GolonganDTO>(result);
      }
    }
  }
}
