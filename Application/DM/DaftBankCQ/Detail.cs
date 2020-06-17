using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.DaftBankCQ
{
  public class Detail
  {
    public class Query : IRequest<DaftBank>
    {
      public string KdBank { get; set; }
    }

    public class Handler : IRequestHandler<Query, DaftBank>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DaftBank> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.DaftBank.FindAsync(x => x.KdBank == request.KdBank);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
