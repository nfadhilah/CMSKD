using AutoMapper;
using AutoWrapper.Wrappers;
using FluentValidation;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.User
{
  public class Delete
  {

    public class Command : IRequest
    {
      public int Id { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(x => x.Id).NotNull();
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
        var model = await _context.AppUser.FindByIdAsync(request.Id);

        if (model == null)
          throw new ApiException("Not found",
            (int)HttpStatusCode.NotFound);

        _context.AppUser.Delete(model);

        return Unit.Value;
      }
    }
  }
}
