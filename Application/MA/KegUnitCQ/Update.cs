﻿using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.MA;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.MA.KegUnitCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdUnit { get; set; }
      public long IdKeg { get; set; }
      public string KdTahap { get; set; }
      public long IdPrgrm { get; set; }
      public int? NoPrior { get; set; }
      public long IdSifatKeg { get; set; }
      public long? IdPeg { get; set; }
      public DateTime? TglAkhir { get; set; }
      public DateTime? TglAwal { get; set; }
      public Decimal? TargetP { get; set; }
      public string Lokasi { get; set; }
      public Decimal? JumlanMin1 { get; set; }
      public Decimal? Pagu { get; set; }
      public Decimal? JumlahPls1 { get; set; }
      public string Sasaran { get; set; }
      public string KetKeg { get; set; }
      public string IdPrioDa { get; set; }
      public string IdSas { get; set; }
      public string Target { get; set; }
      public string TargetIf { get; set; }
      public string TargetSen { get; set; }
      public string Volume { get; set; }
      public string Volume1 { get; set; }
      public string Satuan { get; set; }
      public Decimal? PaguPlus { get; set; }
      public Decimal? PaguTif { get; set; }
      public DateTime? TglValid { get; set; }
      public DateTime? DateCreate { get; set; }
      public DateTime? DateUpdate { get; set; }

      public DTO()
      {
        var config = new MapperConfiguration(opt =>
        {
          opt.CreateMap<DTO, Command>();
        });

        _mapper = config.CreateMapper();
      }

      public Command MapDTO(Command destination)
      {
        return _mapper.Map(this, destination);
      }
    }

    public class Validator : AbstractValidator<DTO>
    {
      public Validator()
      {
        RuleFor(d => d.IdUnit).NotEmpty();
        RuleFor(d => d.IdKeg).NotEmpty();
        RuleFor(d => d.KdTahap).NotEmpty();
        RuleFor(d => d.IdPrgrm).NotEmpty();
        RuleFor(d => d.IdSifatKeg).NotEmpty();
      }
    }

    public class Command : KegUnit, IRequest
    {
    }

    public class Handler : IRequestHandler<Command>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<Unit> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.KegUnit.FindByIdAsync(request.IdKegUnit);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.KegUnit.Update(updated))
          throw new ApiException("Problem saving changes");

        return Unit.Value;
      }
    }
  }
}