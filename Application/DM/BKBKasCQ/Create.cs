﻿using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.BKBKasCQ
{
  public class Create
  {
    public class Command : IRequest<BKBKasDTO>
    {
      public string NoBBantu { get; set; }
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
        RuleFor(d => d.NoBBantu).NotEmpty();
        RuleFor(d => d.IdUnit).NotEmpty();
        RuleFor(d => d.IdRek).NotEmpty();
        RuleFor(d => d.IdBank).NotEmpty();
        RuleFor(d => d.NmBKas).NotEmpty();
        RuleFor(d => d.NoRek).NotEmpty();
        RuleFor(d => d.Saldo).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, BKBKasDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BKBKasDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<BkBKas>(request);

        if (!await _context.BkBKas.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.BkBKas.FindAllAsync<DaftUnit, DaftRekening>(
          x => x.NoBBantu == request.NoBBantu, x => x.DaftUnit,
          x => x.DaftRekening);

        return _mapper.Map<BKBKasDTO>(result.SingleOrDefault());
      }
    }
  }
}