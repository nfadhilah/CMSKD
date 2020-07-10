﻿using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.DaftPhk3CQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long? IdUnit { get; set; }
      public string NmPhk3 { get; set; }
      public string NmInst { get; set; }
      public int IdBank { get; set; }
      public string CabangBank { get; set; }
      public string AlamatBank { get; set; }
      public string NoRekBank { get; set; }
      public int IdJUsaha { get; set; }
      public string Alamat { get; set; }
      public string Telepon { get; set; }
      public string NPWP { get; set; }
      public int StValid { get; set; }
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
        RuleFor(d => d.NmPhk3).NotEmpty();
        RuleFor(d => d.NmInst).NotEmpty();
        RuleFor(d => d.IdBank).NotEmpty();
        RuleFor(d => d.CabangBank).NotEmpty();
        RuleFor(d => d.AlamatBank).NotEmpty();
        RuleFor(d => d.NoRekBank).NotEmpty();
        RuleFor(d => d.IdJUsaha).NotEmpty();
        RuleFor(d => d.NPWP).NotEmpty();
      }
    }

    public class Command : DaftPhk3, IRequest<DaftPhk3DTO>
    {
    }

    public class Handler : IRequestHandler<Command, DaftPhk3DTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DaftPhk3DTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.DaftPhk3.FindByIdAsync(request.IdPhk3);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.DaftPhk3.Update(updated))
          throw new ApiException("Problem saving changes");

        var result = await _context.DaftPhk3
          .FindAllAsync<JBank, JUsaha>(x => x.IdPhk3 == updated.IdPhk3,
            x => x.Bank, x => x.JUsaha);

        return _mapper.Map<DaftPhk3DTO>(result.SingleOrDefault());
      }
    }
  }
}