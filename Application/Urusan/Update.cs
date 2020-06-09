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
  public class Update
  {
    public class Command : IRequest
    {
      public long IdUrusanUnit { get; set; }
      public long IdUnit { get; set; }
      public long UrusKey { get; set; }
    }

    public class Validator : AbstractValidator<UrusanUnit>
    {
      public Validator()
      {
        RuleFor(x => x.IdUnit).NotEmpty();
        RuleFor(x => x.UrusKey).NotEmpty();
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
        var updated = await _context.UrusanUnit.FindAsync(x =>
          x.IdUnit == request.IdUnit && x.IdUrusanUnit == request.IdUrusanUnit);

        if (updated == null) throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.UrusanUnit.Update(updated))
          throw new ApiException("Problem saving changes");

        return Unit.Value;
      }
    }
  }
}