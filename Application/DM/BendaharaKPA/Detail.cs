using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;

namespace Application.DM.BendaharaKPA
{
  public class Detail
  {
    public class Query : IRequest<BendKPA>
    {
      public long IdBendKPA { get; set; }
    }

    public class Handler : IRequestHandler<Query, BendKPA>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BendKPA> Handle(
      Query request, CancellationToken cancellationToken)
      {
        var result =
          await _context.BendKPA.FindByIdAsync(request.IdBendKPA);

        if (result == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        return result;
      }
    }
  }
}
