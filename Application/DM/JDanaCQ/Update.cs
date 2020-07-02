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

namespace Application.DM.JDanaCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public string NmDana { get; set; }
      public string Ket { get; set; }

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
        RuleFor(d => d.NmDana).NotEmpty();
        RuleFor(d => d.Ket).NotEmpty();
      }
    }

    public class Command : JDana, IRequest<JDana>
    {
    }

    public class Handler : IRequestHandler<Command, JDana>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JDana> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.JDana.FindAsync(x => x.IdJDana == request.IdJDana);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.JDana.Update(updated))
          throw new ApiException("Problem saving changes");

        return updated;
      }
    }
  }
}