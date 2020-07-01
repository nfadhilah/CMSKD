using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.TUBEND;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.BeritaDetRCQ
{
  public class Detail
  {
    public class Query : IRequest<BeritaDetRDTO>
    {
      public long IdBeritaDet { get; set; }
    }

    public class Handler : IRequestHandler<Query, BeritaDetRDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BeritaDetRDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result = await _context.BeritaDetR
          .FindAllAsync<Berita, DaftRekening>(
            x => x.IdBeritaDet == request.IdBeritaDet, x => x.Berita,
            x => x.Rekening);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<BeritaDetRDTO>(result);
      }
    }
  }
}
