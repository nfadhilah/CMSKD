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

namespace Application.TUBEND.STSDetBCQ
{
  public class Detail
  {
    public class Query : IRequest<STSDetBDTO>
    {
      public long IdSTSDetB { get; set; }
    }

    public class Handler : IRequestHandler<Query, STSDetBDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<STSDetBDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.STSDetB
            .FindAllAsync<STS, DaftRekening>(
              x => x.IdSTSDetB == request.IdSTSDetB,
              x => x.STS, x => x.Rekening)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<STSDetBDTO>(result);
      }
    }
  }
}
