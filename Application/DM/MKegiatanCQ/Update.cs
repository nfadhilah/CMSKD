using Application.Interfaces;
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

namespace Application.DM.MKegiatanCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdPrgrm { get; set; }
      public string KdPerspektif { get; set; }
      public string NuKeg { get; set; }
      public string NmKegUnit { get; set; }
      public int LevelKeg { get; set; }
      public string Type { get; set; }
      public long? IdKegInduk { get; set; }
      public bool? StAktif { get; set; }
      public bool? StValid { get; set; }

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
        RuleFor(d => d.IdPrgrm).NotEmpty();
        RuleFor(d => d.KdPerspektif).NotEmpty();
        RuleFor(d => d.NuKeg).NotEmpty();
        RuleFor(d => d.NmKegUnit).NotEmpty();
        RuleFor(d => d.LevelKeg).NotEmpty();
        RuleFor(d => d.Type).NotEmpty();
      }
    }

    public class Command : MKegiatan, IRequest<MKegiatanDTO>
    {
    }

    public class Handler : IRequestHandler<Command, MKegiatanDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<MKegiatanDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.MKegiatan.FindAsync(m => m.IdKeg == request.IdKeg);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.MKegiatan.Update(updated))
          throw new ApiException("Problem saving changes");

        var result = await
          _context.MKegiatan.FindAllAsync<MPgrm>(x => x.IdKeg == updated.IdKeg,
            x => x.Program);

        return _mapper.Map<MKegiatanDTO>(result.Single());
      }
    }
  }
}