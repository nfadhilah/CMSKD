﻿using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.KontrakCQ
{
  public class Create
  {
    public class Command : IRequest<Kontrak>
    {
      public long IdUnit { get; set; }
      public string NoKontrak { get; set; }
      public long IdPhk3 { get; set; }
      public long IdKeg { get; set; }
      public DateTime? TglKon { get; set; }
      public DateTime? TglAwalKontrak { get; set; }
      public DateTime? TglAkhirKontrak { get; set; }
      public DateTime? TglSlsKontrak { get; set; }
      public string Uraian { get; set; }
      public decimal? Nilai { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdUnit).NotEmpty();
        RuleFor(d => d.NoKontrak).NotEmpty();
        RuleFor(d => d.IdPhk3).NotEmpty();
        RuleFor(d => d.IdKeg).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, Kontrak>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<Kontrak> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<Kontrak>(request);

        if (!await _context.Kontrak.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}