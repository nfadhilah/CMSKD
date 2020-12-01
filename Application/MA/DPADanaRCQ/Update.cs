﻿using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.MA;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MA.DPADanaRCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdDPAR { get; set; }
      public string KdDana { get; set; }
      public Decimal? Nilai { get; set; }
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
        RuleFor(d => d.IdDPAR).NotEmpty();
        RuleFor(d => d.KdDana).NotEmpty();
      }
    }

    public class Command : DPADanaR, IRequest<DPADanaRDTO>
    {
    }

    public class Handler : IRequestHandler<Command, DPADanaRDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DPADanaRDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.DPADanaR.FindByIdAsync(request.IdDPADanaR);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.DPADanaR.Update(updated))
          throw new ApiException("Problem saving changes");

        var result = await _context.DPADanaR.FindAllAsync<DPAR, JDana>(
          x => x.IdDPADanaR == updated.IdDPADanaR, x => x.DPAR, x => x.JDana);

        return _mapper.Map<DPADanaRDTO>(result.Single());
      }
    }
  }
}