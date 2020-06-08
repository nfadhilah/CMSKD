using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Urusan
{
  public class Delete
  {
    public class Command : IRequest
    {
      public long IdUrusanUnit { get; set; }
      public long IdUnit { get; set; }
    }

    public class Validator : AbstractValidator<UrusanUnit>
    {
      public Validator()
      {
        RuleFor(x => x.IdUrusanUnit).NotEmpty();
        RuleFor(x => x.IdUnit).NotEmpty();
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

      public async Task<Unit> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var deleted = await _context.UrusanUnit.FindAsync(x =>
          x.IdUnit == request.IdUnit && x.IdUrusanUnit == request.IdUrusanUnit);

        if (deleted == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        if (!_context.UrusanUnit.Delete(deleted))
          throw new ApiException("Problem saving changes");

        return Unit.Value;
      }
    }
  }
}