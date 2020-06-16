using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;

namespace Application.DM.JenisArusKas
{
  public class Detail
  {

    public class Query : IRequest<JAKas>
    {
      public long IdKas { get; set; }
    }

    public class Handler : IRequestHandler<Query, JAKas>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JAKas> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.JAKas.FindByIdAsync(request.IdKas);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
