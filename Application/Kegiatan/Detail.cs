using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Kegiatan
{
  public class Detail
  {
    public class Query : IRequest<MKegiatan>
    {
      public long IdKeg { get; set; }
    }

    public class Handler : IRequestHandler<Query, MKegiatan>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<MKegiatan> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.MKegiatan.FindAsync(m => m.IdKeg == request.IdKeg);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
