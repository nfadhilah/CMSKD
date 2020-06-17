using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.DM.JabTtdCQ
{
  public class Create
  {
    public class Command : IRequest<JabTtd>
    {
      public long IdUnit { get; set; }
      public long IdPeg { get; set; }
      public string KdDok { get; set; }
      public string Jabatan { get; set; }
      public string NoSKPTtd { get; set; }
      public DateTime? TglSKPTtd { get; set; }
      public string NoSKStopTtd { get; set; }
      public DateTime? TglSKStopTtd { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdUnit).NotEmpty();
        RuleFor(d => d.IdPeg).NotEmpty();
        RuleFor(d => d.KdDok).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, JabTtd>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JabTtd> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<JabTtd>(request);

        if (!await _context.JabTtd.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}