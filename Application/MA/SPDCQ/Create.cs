﻿using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.MA;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MA.SPDCQ
{
  public class Create
  {
    public class Command : IRequest<SPDDTO>
    {
      public long IdUnit { get; set; }
      public string NoSPD { get; set; }
      public DateTime? TglSPD { get; set; }
      public int KdBulan1 { get; set; }
      public int KdBulan2 { get; set; }
      public int IdxKode { get; set; }
      public string Keterangan { get; set; }
      public DateTime? TglValid { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdUnit).NotEmpty();
        RuleFor(d => d.NoSPD).NotEmpty();
        RuleFor(d => d.TglSPD).NotEmpty();
        RuleFor(d => d.KdBulan1).NotEmpty();
        RuleFor(d => d.KdBulan2).NotEmpty();
        RuleFor(d => d.IdxKode).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, SPDDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPDDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<SPD>(request);

        if (!await _context.SPD.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result =
          await _context.SPD.FindAllAsync<DaftUnit>(x => x.IdSPD == added.IdSPD,
            x => x.Unit);

        return _mapper.Map<SPDDTO>(result.Single());
      }
    }
  }
}