using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.JenisTransaksi
{
  public class Detail
  {

    public class Query : IRequest<JTrans>
    {
      public long IdJTrans { get; set; }
    }

    public class Handler : IRequestHandler<Query, JTrans>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JTrans> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.JTrans.FindByIdAsync(request.IdJTrans);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
