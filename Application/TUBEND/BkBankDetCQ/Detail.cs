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

namespace Application.TUBEND.BkBankDetCQ
{
  public class Detail
  {
    public class Query : IRequest<BKBankDetDTO>
    {
      public long IdBankDet { get; set; }
    }

    public class Handler : IRequestHandler<Query, BKBankDetDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BKBankDetDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result = (await _context.BkBankDet
          .FindAllAsync<BkBank, JTrnlKas>(x => x.IdBankDet == request.IdBankDet,
            x => x.BkBank,
            x => x.JTrnlKas)).SingleOrDefault();

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<BKBankDetDTO>(result);
      }
    }
  }
}
