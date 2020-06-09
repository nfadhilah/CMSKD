using AutoMapper;
using AutoWrapper.Wrappers;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DaftarProfil
{
  public class Update
  {
    public class Command : IRequest
    {
      public long IdProfil { get; set; }
      public string KdProfil { get; set; }
      public string NmProfil { get; set; }
      public DateTime? DateCreate { get; set; }
      public DateTime? DateUpdate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdProfil).NotEmpty();
        RuleFor(d => d.KdProfil).NotEmpty();
        RuleFor(d => d.NmProfil).NotEmpty();
        RuleFor(d => d.DateCreate).NotEmpty();
        RuleFor(d => d.DateUpdate).NotEmpty();
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
          await _context.Profil.FindAsync(x => x.IdProfil == request.IdProfil);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.Profil.Update(updated))
          throw new ApiException("Problem saving changes");

        return Unit.Value;
      }
    }
  }
}