using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;

namespace Application.DM.DaftFungsiCQ
{
  public class Detail
  {

    public class Query : IRequest<DaftFungsi>
    {
      public long IdFung { get; set; }
    }

    public class Handler : IRequestHandler<Query, DaftFungsi>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DaftFungsi> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.DaftFungsi.FindByIdAsync(request.IdFung);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
