using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.DM.JPekerjaanCQ
{
  public class Delete
  {
    public class Command : IRequest
    {
      public long IdJnsPekerjaan { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator() { }
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
          await _context.JPekerjaan.FindByIdAsync(request.IdJnsPekerjaan);

        if (deleted == null)
          throw new ApiException("Not found", (int) HttpStatusCode.NotFound);

        if (!_context.JPekerjaan.Delete(deleted))
          throw new ApiException("Problem saving changes");

        return Unit.Value;
      }
    }
  }
}