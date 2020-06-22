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

namespace Application.TUBEND.STSCQ
{
  public class Detail
  {
    public class Query : IRequest<STS>
    {
      public long IdSTS { get; set; }
    }

    public class Handler : IRequestHandler<Query, STS>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<STS> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          (await _context.STS
            .FindAllAsync<DaftUnit, Bend>(
              x => x.IdSTS == request.IdSTS,
              x => x.Unit, x => x.Bend)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
