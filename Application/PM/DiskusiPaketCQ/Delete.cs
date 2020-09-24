using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.PM.DiskusiPaketCQ
{
  public class Delete
  {
    public class Command : IRequest
    {
      public long IdDiskusiPaket { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(x => x.IdDiskusiPaket).NotEmpty();
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
        var deleted = await _context.DiskusiPaket.FindByIdAsync(request.IdDiskusiPaket);

        if (deleted == null)
          throw new ApiException("Not Found", (int)HttpStatusCode.NotFound);

        if (!_context.DiskusiPaket.Delete(deleted))
          throw new ApiException("Problem saving changes");

        return Unit.Value;
      }
    }
  }
}