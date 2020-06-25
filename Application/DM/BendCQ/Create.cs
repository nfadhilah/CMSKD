using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.BendCQ
{
  public class Create
  {
    public class Command : IRequest<Bend>
    {
      public string JnsBend { get; set; }
      public long IdPeg { get; set; }
      public string IdBank { get; set; }
      public string NmCabBank { get; set; }
      public string RekBend { get; set; }
      public string NPWPBend { get; set; }
      public string JabBend { get; set; }
      public Decimal? SaldoBend { get; set; }
      public Decimal? SaldoBendT { get; set; }
      public DateTime? TglStopBend { get; set; }
      public string WargaNegara { get; set; }
      public int? StAktif { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdPeg).NotEmpty();
        RuleFor(d => d.JnsBend).NotEmpty();
        RuleFor(d => d.RekBend).NotEmpty();
        RuleFor(d => d.IdBank).NotEmpty();
        RuleFor(d => d.JabBend).NotEmpty();
        RuleFor(d => d.NPWPBend).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, Bend>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<Bend> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<Bend>(request);

        if (!await _context.Bend.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}