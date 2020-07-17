using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.BUD;
using Domain.DM;
using Domain.MA;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BUD.SP2DCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public string NoSP2D { get; set; }
      public long IdUnit { get; set; }
      public string KdStatus { get; set; }
      public long IdSPM { get; set; }
      public string NoSPM { get; set; }
      public long? IdBend { get; set; }
      public long? IdSPD { get; set; }
      public long? IdPhk3 { get; set; }
      public long? IdTtd { get; set; }
      public int? IdxKode { get; set; }
      public string NoReg { get; set; }
      public string KetOtor { get; set; }
      public long? IdKontrak { get; set; }
      public string Keperluan { get; set; }
      public string Penolakan { get; set; }
      public DateTime? TglValid { get; set; }
      public DateTime? TglSP2D { get; set; }
      public DateTime? TglSPM { get; set; }
      public string NoBBantu { get; set; }

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
        RuleFor(d => d.IdUnit).NotEmpty();
        RuleFor(d => d.NoSP2D).NotEmpty();
      }
    }

    public class Command : SP2D, IRequest<SP2DDTO>
    {
    }

    public class Handler : IRequestHandler<Command, SP2DDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SP2DDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.SP2D.FindByIdAsync(request.IdSP2D);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.SP2D.Update(updated))
          throw new ApiException("Problem saving changes");

        var result = await _context.SP2D
          .FindAllAsync<DaftUnit, Bend, SPD, DaftPhk3, JabTtd, Kontrak>(
            x => x.IdSPD == updated.IdSP2D, x => x.Unit, x => x.Bend, x => x.SPD,
            x => x.Phk3, x => x.JabTtd, x => x.Kontrak);

        return _mapper.Map<SP2DDTO>(result);
      }
    }
  }
}