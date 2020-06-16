using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.DM.RekeningKas
{
  public class Create
  {
    public class Command : IRequest<BkBKas>
    {
      // public long IdKas { get; set; }
      public long IdUnit { get; set; }
      public long IdRek { get; set; }
      public long IdBank { get; set; }
      public string NmBKas { get; set; }
      public string NoRek { get; set; }
      public Decimal? Saldo { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        // RuleFor(d => d.IdKas).NotEmpty();
        RuleFor(d => d.IdUnit).NotEmpty();
        RuleFor(d => d.IdRek).NotEmpty();
        RuleFor(d => d.IdBank).NotEmpty();
        RuleFor(d => d.NmBKas).NotEmpty();
        RuleFor(d => d.NoRek).NotEmpty();
        RuleFor(d => d.Saldo).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, BkBKas>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BkBKas> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<BkBKas>(request);

        if (!await _context.BkBKas.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}