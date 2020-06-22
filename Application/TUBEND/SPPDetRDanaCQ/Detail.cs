using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.SPPDetRDanaCQ
{
  public class Detail
  {
    public class Query : IRequest<SPPDetRDana>
    {
      public long IdSPPDetRDana { get; set; }
    }

    public class Handler : IRequestHandler<Query, SPPDetRDana>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPPDetRDana> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.SPPDetRDana.FindByIdAsync(request.IdSPPDetRDana);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
