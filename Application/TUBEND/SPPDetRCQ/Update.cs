using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.SPPDetRCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdRek { get; set; }
      public long IdKeg { get; set; }
      public long IdSPP { get; set; }
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
        RuleFor(d => d.IdKeg).NotEmpty();
        RuleFor(d => d.IdRek).NotEmpty();
        RuleFor(d => d.IdSPP).NotEmpty();
        RuleFor(d => d.IdNoJeTra).NotEmpty();
      }
    }

    public class Command : SPPDetR, IRequest<SPPDetRDTO>
    {
    }

    public class Handler : IRequestHandler<Command, SPPDetRDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPPDetRDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.SPPDetR.FindByIdAsync(request.IdSPPDetR);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.SPPDetR.Update(updated))
          throw new ApiException("Problem saving changes");

        var result = await _context.SPPDetR
          .FindAllAsync<DaftRekening, MKegiatan, SPP, JTrnlKas>(
            x => x.IdSPPDetR == updated.IdSPPDetR, x => x.Rekening,
            x => x.Kegiatan, x => x.SPP, x => x.JTrnlKas);

        return _mapper.Map<SPPDetRDTO>(result.SingleOrDefault());
      }
    }
  }
}