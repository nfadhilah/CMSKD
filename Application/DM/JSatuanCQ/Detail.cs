using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;

namespace Application.DM.JSatuanCQ
{
  public class Detail
  {

    public class Query : IRequest<JSatuan>
    {
      public long IdSatuan { get; set; }
    }

    public class Handler : IRequestHandler<Query, JSatuan>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JSatuan> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.JSatuan.FindAsync(x => x.IdSatuan == request.IdSatuan);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
