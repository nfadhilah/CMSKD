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

namespace Application.DM.StatTrsCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public string LblStatus { get; set; }
      public string Uraian { get; set; }

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
        RuleFor(d => d.LblStatus).NotEmpty();
        RuleFor(d => d.Uraian).NotEmpty();
      }
    }

    public class Command : StatTrs, IRequest<StatTrs>
    {
    }

    public class Handler : IRequestHandler<Command, StatTrs>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<StatTrs> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
            await _context.StatTrs.FindAsync(x => x.KdStatus == request.KdStatus);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.StatTrs.Update(updated))
          throw new ApiException("Problem saving changes");

        return updated;
      }
    }
  }
}
