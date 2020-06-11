using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UnitOrganisasi
{
  public class Detail
  {
    public class Query : IRequest<DaftUnit>
    {
      public int IdUnit { get; set; }
    }

    public class Handler : IRequestHandler<Query, DaftUnit>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DaftUnit> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.DaftUnit.FindByIdAsync(request.IdUnit);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
