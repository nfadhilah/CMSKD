using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;

namespace Application.DM.DaftarProfil
{
  public class Detail
  {

    public class Query : IRequest<Profil>
    {
      public long IdProfil { get; set; }
    }

    public class Handler : IRequestHandler<Query, Profil>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<Profil> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.Profil.FindAsync(x => x.IdProfil == request.IdProfil);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
