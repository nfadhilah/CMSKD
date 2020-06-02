using AutoMapper;
using AutoWrapper.Wrappers;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Bendahara
{
  public class Update
  {
    public class Command : IRequest
    {
    public string KeyBend { get; set; }
    public string UnitKey { get; set; }
    public string NIP { get; set; }
    public string Jns_Bend { get; set; }
    public int StAktif { get; set; }
    public string KdBank { get; set; }
    public string Jab_Bend { get; set; }
    public string RekBend { get; set; }
    public string NPWPBend { get; set; }
    public Decimal? SaldoBend { get; set; }
    public Decimal? SaldoBendT { get; set; }
    public DateTime? TglStopBend { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.KeyBend).NotEmpty();
        RuleFor(d => d.NIP).NotEmpty();
        RuleFor(d => d.Jns_Bend).NotEmpty();
        RuleFor(d => d.RekBend).NotEmpty();
        RuleFor(d => d.KdBank).NotEmpty();
        RuleFor(d => d.Jab_Bend).NotEmpty();
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
          await _context.Bend.FindByIdAsync(request.KeyBend);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.Bend.Update(updated))
          throw new ApiException("Problem saving changes");

        return Unit.Value;
      }
    }
  }
}