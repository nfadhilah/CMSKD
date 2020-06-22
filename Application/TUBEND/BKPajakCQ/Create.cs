using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.BKPajakCQ
{
  public class Create
  {
    public class Command : IRequest<BkPajak>
    {
      public long IdUnit { get; set; }
      public long IdBend { get; set; }
      public string NoBkPajak { get; set; }
      public int IdxKode { get; set; }
      public string KdStatus { get; set; }
      public DateTime? TglBkPajak { get; set; }
      public string Uraian { get; set; }
      public DateTime? TglValid { get; set; }
      public long? KdRilis { get; set; }
      public int? StKirim { get; set; }
      public int? StCair { get; set; }
      public int IdTransfer { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdUnit).NotEmpty();
        RuleFor(d => d.IdBend).NotEmpty();
        RuleFor(d => d.NoBkPajak).NotEmpty();
        RuleFor(d => d.KdStatus).NotEmpty();
        RuleFor(d => d.IdxKode).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, BkPajak>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BkPajak> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<BkPajak>(request);

        if (!await _context.BkPajak.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}