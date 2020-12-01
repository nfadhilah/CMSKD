﻿using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.JTrnlKasCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public string NmJeTra { get; set; }
      public string KdPers { get; set; }

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
        RuleFor(d => d.NmJeTra).NotEmpty();
        RuleFor(d => d.KdPers).NotEmpty();
      }
    }

    public class Command : JTrnlKas, IRequest<JTrnlKas>
    {
    }

    public class Handler : IRequestHandler<Command, JTrnlKas>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JTrnlKas> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.JTrnlKas.FindByIdAsync(request.IdNoJeTra);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.JTrnlKas.Update(updated))
          throw new ApiException("Problem saving changes");

        return updated;
      }
    }
  }
}