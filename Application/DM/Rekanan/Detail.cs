using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;

namespace Application.DM.Rekanan
{
  public class Detail
  {
    public class Query : IRequest<DaftPhk3>
    {
      public int IdPhk3 { get; set; }
    }

    public class Handler : IRequestHandler<Query, DaftPhk3>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DaftPhk3> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.DaftPhk3.FindByIdAsync(request.IdPhk3);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
