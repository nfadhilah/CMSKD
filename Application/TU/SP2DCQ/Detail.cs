using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Auth.WebUserCQ;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.Auth;
using Domain.DM;
using MediatR;
using Persistence;

namespace Application.TU.SP2DCQ
{
  public class Detail
  {
    public class Query : IRequest<SP2DDTO>
    {
      public string NoSP2D { get; set; }
    }

    public class Handler : IRequestHandler<Query, SP2DDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SP2DDTO> Handle(
        Query request, CancellationToken cancellationToken)
      {
        var result = await _context.SP2D.FindAsync<DaftUnit, StatTrs>(
          x => x.NoSP2D == WebUtility.UrlDecode(request.NoSP2D), x => x.DaftUnit, x => x.StatTrs);

        if (result == null)
          throw new ApiException("Not found",
            (int) HttpStatusCode.NotFound);

        return _mapper.Map<SP2DDTO>(result);
      }
    }
  }
}