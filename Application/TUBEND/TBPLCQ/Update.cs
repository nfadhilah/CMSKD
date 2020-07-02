﻿using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.TBPLCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdUnit { get; set; }
      public string NoTBPL { get; set; }
      public long IdBend { get; set; }
      public string KdStatus { get; set; }
      public int IdxKode { get; set; }
      public DateTime? TglTBPL { get; set; }
      public string Penyetor { get; set; }
      public string Alamat { get; set; }
      public string UraiTBPL { get; set; }
      public DateTime? TglValid { get; set; }
      public long? KdRilis { get; set; }
      public int? StKirim { get; set; }
      public int? StCair { get; set; }

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
        RuleFor(d => d.NoTBPL).NotEmpty();
        RuleFor(d => d.KdStatus).NotEmpty();
        RuleFor(d => d.IdxKode).NotEmpty();
      }
    }

    public class Command : TBPL, IRequest<TBPLDTO>
    {
    }

    public class Handler : IRequestHandler<Command, TBPLDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<TBPLDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.TBPL.FindByIdAsync(request.IdTBPL);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.TBPL.Update(updated))
          throw new ApiException("Problem saving changes");

        var result = await _context.TBPL
          .FindAllAsync<DaftUnit, StatTrs, Bend, ZKode>(
            x => x.IdTBPL == updated.IdTBPL, x => x.Unit,
            x => x.StatTrs, x => x.Bend, x => x.ZKode);

        return _mapper.Map<TBPLDTO>(result.SingleOrDefault());
      }
    }
  }
}