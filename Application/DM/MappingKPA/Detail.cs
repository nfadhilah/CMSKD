using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;

namespace Application.DM.MappingKPA
{
  public class Detail
  {
    public class Query : IRequest<KPA>
    {
      public long IdUnit { get; set; }
      public long IdPeg { get; set; }
    }

    public class Handler : IRequestHandler<Query, KPA>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<KPA> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var query = await _context.KPA
          .FindAllAsync<DaftUnit, Pegawai>(
            u => u.IdUnit == request.IdUnit && u.IdPeg == request.IdPeg,
            u => u.DaftUnit, u => u.Pegawai);

        var result = query.SingleOrDefault();

        if (result == null) throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
