using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.BkBankCQ
{
  public class Detail
  {
    public class Query : IRequest<BKBankDTO>
    {
      public long IdBkBank { get; set; }
    }

    public class Handler : IRequestHandler<Query, BKBankDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BKBankDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result = (await _context.BkBank
          .FindAllAsync<DaftUnit, Bend, StatTrs>(
            x => x.IdBkBank == request.IdBkBank, x => x.Unit,
            x => x.Bend, x => x.StatTrs)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<BKBankDTO>(result);
      }
    }
  }
}
