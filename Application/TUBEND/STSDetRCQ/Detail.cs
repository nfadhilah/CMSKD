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

namespace Application.TUBEND.STSDetRCQ
{
  public class Detail
  {
    public class Query : IRequest<STSDetRDTO>
    {
      public long IdSTSDetR { get; set; }
    }

    public class Handler : IRequestHandler<Query, STSDetRDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<STSDetRDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.STSDetR
            .FindAllAsync<STS, MKegiatan, DaftRekening>(
              x => x.IdSTSDetR == request.IdSTSDetR,
              x => x.STS, x => x.Kegiatan, x => x.Rekening)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<STSDetRDTO>(result);
      }
    }
  }
}
