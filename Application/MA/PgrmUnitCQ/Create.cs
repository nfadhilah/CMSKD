using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.MA;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MA.PgrmUnitCQ
{
  public class Create
  {
    public class Command : IRequest<PgrmUnit>
    {
      public long IdUnit { get; set; }
      public string KdTahap { get; set; }
      public long IdPrgrm { get; set; }
      public string Target { get; set; }
      public string Sasaran { get; set; }
      public int? NoPrio { get; set; }
      public string Indikator { get; set; }
      public string Ket { get; set; }
      public string IdSas { get; set; }
      public DateTime? TglValid { get; set; }
      public int? IdXKode { get; set; }
      public DateTime? DateCreate { get; set; }
      public DateTime? DateUpdate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdUnit).NotEmpty();
        RuleFor(d => d.KdTahap).NotEmpty();
        RuleFor(d => d.IdPrgrm).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, PgrmUnit>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<PgrmUnit> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<PgrmUnit>(request);

        if (!await _context.PgrmUnit.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}