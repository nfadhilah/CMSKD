﻿using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.JabTtdCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdUnit { get; set; }
      public long IdPeg { get; set; }
      public string KdDok { get; set; }
      public string Jabatan { get; set; }
      public string NoSKPTtd { get; set; }
      public DateTime? TglSKPTtd { get; set; }
      public string NoSKStopTtd { get; set; }
      public DateTime? TglSKStopTtd { get; set; }

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
        RuleFor(d => d.IdPeg).NotEmpty();
        RuleFor(d => d.KdDok).NotEmpty();
      }
    }

    public class Command : JabTtd, IRequest<JabTTdDTO>
    {
    }

    public class Handler : IRequestHandler<Command, JabTTdDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JabTTdDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.JabTtd.FindByIdAsync(request.IdTtd);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.JabTtd.Update(updated))
          throw new ApiException("Problem saving changes");

        var result = await _context.JabTtd
          .FindAllAsync<DaftUnit, Pegawai>(x => x.IdTtd == updated.IdTtd,
            x => x.DaftUnit, x => x.Pegawai);

        return _mapper.Map<JabTTdDTO>(result);
      }
    }
  }
}