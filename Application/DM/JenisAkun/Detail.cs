using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;

namespace Application.DM.JenisAkun
{
  public class Detail
  {

    public class Query : IRequest<JnsAkun>
    {
      public long IdJnsAkun { get; set; }
    }

    public class Handler : IRequestHandler<Query, JnsAkun>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JnsAkun> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.JnsAkun.FindByIdAsync(request.IdJnsAkun);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
