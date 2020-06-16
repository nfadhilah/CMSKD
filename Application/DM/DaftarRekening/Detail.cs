using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;

namespace Application.DM.DaftarRekening
{
  public class Detail
  {
    public class Query : IRequest<DaftRekening>
    {
      public long IdRek { get; set; }
    }

    public class Handler : IRequestHandler<Query, DaftRekening>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DaftRekening> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.DaftRekening.FindByIdAsync(request.IdRek);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
