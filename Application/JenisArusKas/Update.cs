using AutoMapper;
using AutoWrapper.Wrappers;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.JenisArusKas
{
  public class Update
  {
    public class Command : IRequest
    {
      public long IdKas { get; set; }
      public string KdAKas { get; set; }
      public string NmAKas { get; set; }
      public string LabelKas { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdKas).NotEmpty();
        RuleFor(d => d.KdAKas).NotEmpty();
        RuleFor(d => d.NmAKas).NotEmpty();
        RuleFor(d => d.LabelKas).NotEmpty();
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
          await _context.JAKas.FindByIdAsync(request.IdKas);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.JAKas.Update(updated))
          throw new ApiException("Problem saving changes");

        return Unit.Value;
      }
    }
  }
}