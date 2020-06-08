using AutoMapper;
using AutoWrapper.Wrappers;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.JenisDana
{
  public class Update
  {
    public class Command : IRequest
    {
    public long IdJDana { get; set; }
    public string KdDana { get; set; }
    public string NmDana { get; set; }
    public string Ket { get; set; }
    public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdJDana).NotEmpty();
        RuleFor(d => d.KdDana).NotEmpty();
        RuleFor(d => d.NmDana).NotEmpty();
        RuleFor(d => d.Ket).NotEmpty();
        RuleFor(d => d.DateCreate).NotEmpty();
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
          await _context.JDana.FindByIdAsync(request.IdJDana);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.JDana.Update(updated))
          throw new ApiException("Problem saving changes");

        return Unit.Value;
      }
    }
  }
}