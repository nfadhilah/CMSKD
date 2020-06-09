using AutoMapper;
using AutoWrapper.Wrappers;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.JenisBendahara
{
  public class Update
  {
    public class Command : IRequest
    {
      public long IdJBend { get; set; }
      public string Jns_Bend { get; set; }
      public long IdRek { get; set; }
      public string Urai_Bend { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdJBend).NotEmpty();
        RuleFor(d => d.Jns_Bend).NotEmpty();
        RuleFor(d => d.IdRek).NotEmpty();
        RuleFor(d => d.Urai_Bend).NotEmpty();
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
        var updated =
          // await _context.JBend.FindByIdAsync(request.IdJBend);
          await _context.JBend.FindAsync(x => x.IdJBend == request.IdJBend);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.JBend.Update(updated))
          throw new ApiException("Problem saving changes");

        return Unit.Value;
      }
    }
  }
}