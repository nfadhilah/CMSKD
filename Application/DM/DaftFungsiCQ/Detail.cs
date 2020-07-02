using AutoMapper;
using AutoWrapper.Wrappers;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.DaftFungsiCQ
{
  public class Detail
  {

    public class Query : IRequest<DaftFungsiDTO>
    {
      public long IdFung { get; set; }
    }

    public class Handler : IRequestHandler<Query, DaftFungsiDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DaftFungsiDTO> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.DaftFungsi.FindByIdAsync(request.IdFung);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return _mapper.Map<DaftFungsiDTO>(result);
      }
    }
  }
}
