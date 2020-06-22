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

namespace Application.TUBEND.STSDetDCQ
{
  public class Detail
  {
    public class Query : IRequest<STSDetD>
    {
      public long IdSTSDetD { get; set; }
    }

    public class Handler : IRequestHandler<Query, STSDetD>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<STSDetD> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.STSDetD
            .FindAllAsync<STS, DaftRekening>(
              x => x.IdSTSDetD == request.IdSTSDetD,
              x => x.STS, x => x.Rekening)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
