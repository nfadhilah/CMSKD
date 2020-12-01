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

namespace Application.DM.JnsAkunCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public string UraiAkun { get; set; }
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
        RuleFor(d => d.UraiAkun).NotEmpty();
        RuleFor(d => d.KdPers).NotEmpty();
      }
    }

    public class Command : JnsAkun, IRequest<JnsAkun>
    {
    }

    public class Handler : IRequestHandler<Command, JnsAkun>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JnsAkun> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.JnsAkun.FindByIdAsync(request.IdJnsAkun);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.JnsAkun.Update(updated))
          throw new ApiException("Problem saving changes");

        return updated;
      }
    }
  }
}