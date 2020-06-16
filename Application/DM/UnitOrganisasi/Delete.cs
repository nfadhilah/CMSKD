using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.DM.UnitOrganisasi
{
  public class Delete
  {

    public class Command : IRequest
    {
      public int IdUnit { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
      }
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

      public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
      {
        var deleted =
          await _context.DaftUnit.FindByIdAsync(request.IdUnit);

        if (deleted == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        if (!_context.DaftUnit.Delete(deleted))
          throw new ApiException("Problem saving changes");

        return Unit.Value;
      }
    }
  }
}
