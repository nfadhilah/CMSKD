using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.TUBEND.SPMCQ;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.MA;
using Domain.TUBEND;
using MediatR;
using Persistence;

namespace Application.PM.PaketRUPCQ
{
  public class Detail
  {
    public class Query : IRequest<PaketRUPDTO>
    {
      public long IdRUP { get; set; }
    }

    public class Handler : IRequestHandler<Query, PaketRUPDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<PaketRUPDTO> Handle(
        Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.PaketRup
            .FindAllAsync<DaftUnit, MKegiatan>(x => x.IdRUP == request.IdRUP,
              c => c.Unit,
              c => c.Keg)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int) HttpStatusCode.NotFound);

        return _mapper.Map<PaketRUPDTO>(result);
      }
    }
  }
}