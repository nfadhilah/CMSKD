using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.PegawaiCQ
{
  public class Detail
  {

    public class Query : IRequest<PegawaiDTO>
    {
      public long IdPeg { get; set; }
    }

    public class Handler : IRequestHandler<Query, PegawaiDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<PegawaiDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.Pegawai.FindAllAsync<DaftUnit, Golongan>(
            x => x.IdPeg == request.IdPeg, c => c.DaftUnit, c => c.Golongan))
          .FirstOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<PegawaiDTO>(result);
      }
    }
  }
}
