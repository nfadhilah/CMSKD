﻿using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using FluentValidation;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MappingKPA
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long NewIdPeg { get; set; }
      public string Jabatan { get; set; }

      public DTO()
      {
        var config = new MapperConfiguration(opt =>
        {
          opt.CreateMap<DTO, Command>()
            .ForMember(d => d.IdPeg, o => o.MapFrom(s => s.NewIdPeg));
        });

        _mapper = config.CreateMapper();
      }

      public Command MapDTO(Command destination) =>
        _mapper.Map(this, destination);
    }

    public class Validator : AbstractValidator<DTO>
    {
      public Validator()
      {
        RuleFor(x => x.NewIdPeg).NotEmpty();
        RuleFor(x => x.Jabatan).NotEmpty();
      }
    }

    public class Command : IRequest
    {
      public long IdKPA { get; set; }
      public long IdUnit { get; set; }
      public long IdPeg { get; set; }
      public string Jabatan { get; set; }
    }

    public class Handler : IRequestHandler<Command>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<Unit> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated = await _context.KPA.FindAsync(x =>
          x.IdUnit == request.IdUnit && x.IdPeg == request.IdPeg);

        if (updated == null) throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.KPA.Update(updated))
          throw new ApiException("Problem saving changes");

        return Unit.Value;
      }
    }
  }
}