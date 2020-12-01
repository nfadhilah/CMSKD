﻿using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.KPACQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdPeg { get; set; }
      public string Jabatan { get; set; }

      public DTO()
      {
        var config = new MapperConfiguration(opt =>
        {
          opt.CreateMap<DTO, Command>()
            .ForMember(d => d.IdPeg, o => o.MapFrom(s => s.IdPeg));
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
        RuleFor(x => x.IdPeg).NotEmpty();
        RuleFor(x => x.Jabatan).NotEmpty();
      }
    }

    public class Command : KPA, IRequest<KPADTO>
    {
    }

    public class Handler : IRequestHandler<Command, KPADTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<KPADTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.KPA.FindAsync(x => x.IdPeg == request.IdPeg);

        if (updated == null) throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.KPA.Update(updated))
          throw new ApiException("Problem saving changes");

        var result = await _context.KPA
          .FindAllAsync<Pegawai>(x => x.IdKPA == updated.IdKPA, x => x.Pegawai);

        return _mapper.Map<KPADTO>(result.Single());
      }
    }
  }
}