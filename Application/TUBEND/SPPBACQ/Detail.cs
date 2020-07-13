using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.SPPBACQ
{
  public class Detail
  {
    public class Query : IRequest<SPPBADTO>
    {
      public long IdSPPBA { get; set; }
    }

    public class Handler : IRequestHandler<Query, SPPBADTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPPBADTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result = await
          _context.SPPBA.FindAllAsync<SPP, Berita>(x => x.IdSPPBA == request.IdSPPBA, x => x.SPP,
            x => x.Berita);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<SPPBADTO>(result.Single());
      }
    }
  }
}
