using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.DM.DafPajak
{
  public class Create
  {
    public class Command : IRequest<Pajak>
    {
      // public long IdPjk { get; set; }
      public string KdPajak { get; set; }
      public string NmPajak { get; set; }
      public string Uraian { get; set; }
      public string Keterangan { get; set; }
      public string RumusPajak { get; set; }
      public int StAktif { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        // RuleFor(d => d.IdPajak).NotEmpty();
        RuleFor(d => d.KdPajak).NotEmpty();
        RuleFor(d => d.NmPajak).NotEmpty();
        RuleFor(d => d.Uraian).NotEmpty();
        RuleFor(d => d.Keterangan).NotEmpty();
        RuleFor(d => d.RumusPajak).NotEmpty();
        RuleFor(d => d.StAktif).NotEmpty();
        RuleFor(d => d.DateCreate).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, Pajak>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<Pajak> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<Pajak>(request);

        if (!await _context.Pajak.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}