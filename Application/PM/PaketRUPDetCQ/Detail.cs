using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;

namespace Application.PM.PaketRUPDetCQ
{
  public class Detail
  {
    public class Query : IRequest<PaketRUPDetDTO>
    {
      public long IdRUPDet { get; set; }
    }

    public class Handler : IRequestHandler<Query, PaketRUPDetDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<PaketRUPDetDTO> Handle(
        Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.PaketRupDet
            .FindAllAsync<JPekerjaan, JDana, DaftPhk3>(
              x => x.IdRUPDet == request.IdRUPDet,
              c => c.JnsPekerjaan,
              c => c.JDana, c => c.Phk3)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int) HttpStatusCode.NotFound);

        return _mapper.Map<PaketRUPDetDTO>(result);
      }
    }
  }
}