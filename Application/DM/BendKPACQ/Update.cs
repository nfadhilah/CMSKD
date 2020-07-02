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

namespace Application.DM.BendKPACQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdBend { get; set; }
      public long IdPeg { get; set; }

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
        RuleFor(d => d.IdBend).NotEmpty();
        RuleFor(d => d.IdPeg).NotEmpty();
      }
    }

    public class Command : IRequest<BendKPADTO>
    {
      public long IdBendKPA { get; set; }
      public long IdBend { get; set; }
      public long IdPeg { get; set; }
    }

    public class Handler : IRequestHandler<Command, BendKPADTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BendKPADTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.BendKPA.FindByIdAsync(request.IdBendKPA);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.BendKPA.Update(updated))
          throw new ApiException("Problem saving changes");

        var result = await _context.BendKPA.FindAllAsync<Bend, Pegawai>(
          x => x.IdBendKPA == updated.IdBendKPA, x => x.Bend, x => x.Pegawai);

        return _mapper.Map<BendKPADTO>(result);
      }
    }
  }
}