using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.MA;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.SPPCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdUnit { get; set; }
      public string NoSPP { get; set; }
      public string KdStatus { get; set; }
      public int KdBulan { get; set; }
      public long? IdBend { get; set; }
      public long IdSPD { get; set; }
      public long? IdPhk3 { get; set; }
      public int IdxKode { get; set; }
      public string NoReg { get; set; }
      public string KetOtor { get; set; }
      public long? IdKontrak { get; set; }
      public string Keperluan { get; set; }
      public string Penolakan { get; set; }
      public DateTime? TglValid { get; set; }
      public DateTime? TglSPP { get; set; }
      public DateTime? Status { get; set; }
      public DateTime? DateCreate { get; set; }

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
        RuleFor(d => d.NoSPP).NotEmpty();
        RuleFor(d => d.KdStatus).NotEmpty();
        RuleFor(d => d.KdBulan).NotEmpty();
        RuleFor(d => d.IdSPD).NotEmpty();
        RuleFor(d => d.IdxKode).NotEmpty();
      }
    }

    public class Command : SPP, IRequest<SPPDTO>
    {
    }

    public class Handler : IRequestHandler<Command, SPPDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPPDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.SPP.FindByIdAsync(request.IdSPP);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.SPP.Update(updated))
          throw new ApiException("Problem saving changes");

        var result = await _context.SPP
          .FindAllAsync<DaftUnit, StatTrs, Bend, SPD, DaftPhk3, ZKode>(
            x => x.IdSPP == updated.IdSPP, x => x.Unit,
            x => x.StatTrs, x => x.Bendahara, x => x.SPD, x => x.Phk3,
            x => x.ZKode);

        return _mapper.Map<SPPDTO>(result.SingleOrDefault());
      }
    }
  }
}