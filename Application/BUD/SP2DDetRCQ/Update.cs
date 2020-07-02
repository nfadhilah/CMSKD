using Application.Interfaces;
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

namespace Application.BUD.SP2DDetRCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdSP2D { get; set; }
      public long IdKeg { get; set; }
      public long IdRek { get; set; }
      public int IdNoJeTra { get; set; }
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
        RuleFor(d => d.IdSP2D).NotEmpty();
        RuleFor(d => d.IdKeg).NotEmpty();
        RuleFor(d => d.IdRek).NotEmpty();
        RuleFor(d => d.IdNoJeTra).NotEmpty();
      }
    }

    public class Command : SP2DDetR, IRequest<SP2DDetRDTO>
    {
    }

    public class Handler : IRequestHandler<Command, SP2DDetRDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SP2DDetRDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.SP2DDetR.FindByIdAsync(request.IdSP2DDetR);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.SP2DDetR.Update(updated))
          throw new ApiException("Problem saving changes");

        var result = await _context.SP2DDetR
          .FindAllAsync<SP2D, MKegiatan, DaftRekening>(
            x => x.IdSP2DDetR == updated.IdSP2DDetR, x => x.SP2D, x => x.Kegiatan,
            x => x.Rekening);

        return _mapper.Map<SP2DDetRDTO>(result.SingleOrDefault());
      }
    }
  }
}