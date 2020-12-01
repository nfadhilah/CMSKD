﻿using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.BUD;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BUD.SP2DDetRDanaCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdSP2DDetR { get; set; }
      public long IdJDana { get; set; }
      public decimal? Nilai { get; set; }

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
        RuleFor(d => d.IdSP2DDetR).NotEmpty();
        RuleFor(d => d.IdJDana).NotEmpty();
      }
    }

    public class Command : SP2DDetRDana, IRequest<SP2DDetRDanaDTO>
    {
    }

    public class Handler : IRequestHandler<Command, SP2DDetRDanaDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SP2DDetRDanaDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.SP2DDetRDana.FindByIdAsync(request.IdSP2DDetRDana);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.SP2DDetRDana.Update(updated))
          throw new ApiException("Problem saving changes");

        var result = await _context.SP2DDetRDana
          .FindAllAsync<SP2DDetR, JDana>(
            x => x.IdSP2DDetR == updated.IdSP2DDetRDana, x => x.SP2DDetR,
            x => x.JDana);

        return _mapper.Map<SP2DDetRDanaDTO>(result.SingleOrDefault());
      }
    }
  }
}