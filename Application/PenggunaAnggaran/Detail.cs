using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.PenggunaAnggaran
{
  public class Detail
  {
    public class Query : IRequest<PA>
    {
      public long IdPA { get; set; }
    }

    public class Handler : IRequestHandler<Query, PA>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<PA> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.PA.FindByIdAsync(request.IdPA);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
