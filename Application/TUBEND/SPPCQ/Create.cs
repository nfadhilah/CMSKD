﻿using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.MA;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.SPPCQ
{
  public class Create
  {
    public class Command : IRequest<SPP>
    {
      public long IdUnit { get; set; }
      public string NoSPP { get; set; }
      public string KdStatus { get; set; }
      public int KdBulan { get; set; }
      public long? IdBend { get; set; }
      public long IdSPD { get; set; }
      public long? IdPhk3 { get; set; }
      public int IdxKode { get; set; }
      public string NoReg { get; set; }
      public string KetOtor { get; set; }
      public string NoKontrak { get; set; }
      public string Keperluan { get; set; }
      public string Penolakan { get; set; }
      public DateTime? TglValid { get; set; }
      public DateTime? TglSPP { get; set; }
      public DateTime? Status { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdUnit).NotEmpty();
        RuleFor(d => d.NoSPP).NotEmpty();
        RuleFor(d => d.KdStatus).NotEmpty();
        RuleFor(d => d.KdBulan).NotEmpty();
        RuleFor(d => d.IdSPD).NotEmpty();
        RuleFor(d => d.IdxKode).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, SPP>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPP> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<SPP>(request);

        if (await _context.SPP.FindAsync(x => x.NoSPP == request.NoSPP) != null)
          throw new ApiException("No. SPP sudah terpakai di daftar SPP");

        if (!await _context.SPP.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.SPP
          .FindAllAsync<DaftUnit, StatTrs, Bend, SPD, DaftPhk3, ZKode>(
            x => x.IdSPP == added.IdSPP, x => x.Unit,
            x => x.StatTrs, x => x.Bendahara, x => x.SPD, x => x.Phk3,
            x => x.ZKode);

        return result.SingleOrDefault();
      }
    }
  }
}