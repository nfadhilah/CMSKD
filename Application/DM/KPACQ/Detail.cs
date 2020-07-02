using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.KPACQ
{
  public class Detail
  {
    public class Query : IRequest<KPADTO>
    {
      public long IdKPA { get; set; }
    }

    public class Handler : IRequestHandler<Query, KPADTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<KPADTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result = (await _context.KPA
            .FindAllAsync<Pegawai>(x => x.IdKPA == request.IdKPA,
              x => x.Pegawai))
          .SingleOrDefault();

        if (result == null) throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<KPADTO>(result);
      }
    }
  }
}
