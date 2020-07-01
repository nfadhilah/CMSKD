using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.TUBEND;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.BeritaCQ
{
  public class Detail
  {
    public class Query : IRequest<BeritaDTO>
    {
      public long IdBerita { get; set; }
    }

    public class Handler : IRequestHandler<Query, BeritaDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BeritaDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.Berita
            .FindAllAsync<DaftUnit, MKegiatan, Kontrak>(
              x => x.IdBerita == request.IdBerita, x => x.Unit,
              x => x.Kegiatan, x => x.Kontrak)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<BeritaDTO>(result);
      }
    }
  }
}
