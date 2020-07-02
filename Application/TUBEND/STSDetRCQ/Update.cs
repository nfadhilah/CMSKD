using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.STSDetRCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdSTS { get; set; }
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
        RuleFor(d => d.IdSTS).NotEmpty();
        RuleFor(d => d.IdRek).NotEmpty();
        RuleFor(d => d.IdNoJeTra).NotEmpty();
      }
    }

    public class Command : STSDetR, IRequest<STSDetRDTO>
    {
    }

    public class Handler : IRequestHandler<Command, STSDetRDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<STSDetRDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.STSDetR.FindByIdAsync(request.IdSTSDetR);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.STSDetR.Update(updated))
          throw new ApiException("Problem saving changes");

        var result = await _context.STSDetR
          .FindAllAsync<STS, MKegiatan, DaftRekening>(
            x => x.IdSTSDetR == updated.IdSTSDetR,
            x => x.STS, x => x.Kegiatan, x => x.Rekening);

        return _mapper.Map<STSDetRDTO>(result);
      }
    }
  }
}