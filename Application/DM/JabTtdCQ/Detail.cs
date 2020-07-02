using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.JabTtdCQ
{
  public class Detail
  {
    public class Query : IRequest<JabTTdDTO>
    {
      public long IdTtd { get; set; }
    }

    public class Handler : IRequestHandler<Query, JabTTdDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JabTTdDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result = (await _context.JabTtd
          .FindAllAsync<DaftUnit, Pegawai>(x => x.IdTtd == request.IdTtd,
            x => x.DaftUnit, x => x.Pegawai)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<JabTTdDTO>(result);
      }
    }
  }
}
