using AutoMapper;
using AutoWrapper.Wrappers;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.STSDetRCQ
{
  public class Delete
  {
    public class Command : IRequest
    {
      public long IdSTSDetR { get; set; }
    }

    public class Handler : IRequestHandler<Command>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<Unit> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var deleted =
          await _context.STSDetR.FindByIdAsync(request.IdSTSDetR);

        if (deleted == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        if (!_context.STSDetR.Delete(deleted))
          throw new ApiException("Problem saving changes");

        return Unit.Value;
      }
    }
  }
}