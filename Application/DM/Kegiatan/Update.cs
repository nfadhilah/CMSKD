using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.DM.Kegiatan
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdPgrm { get; set; }
      public string KdPerspektif { get; set; }
      public string NuKeg { get; set; }
      public string NmKegUnit { get; set; }
      public int LevelKeg { get; set; }
      public string Type { get; set; }
      public string KdKeg_Induk { get; set; }

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
        RuleFor(d => d.IdPgrm).NotEmpty();
        RuleFor(d => d.KdPerspektif).NotEmpty();
        RuleFor(d => d.NuKeg).NotEmpty();
        RuleFor(d => d.NmKegUnit).NotEmpty();
        RuleFor(d => d.LevelKeg).NotEmpty();
        RuleFor(d => d.Type).NotEmpty();
      }
    }

    public class Command : IRequest
    {
      public long IdKeg { get; set; }
      public long IdPgrm { get; set; }
      public string KdPerspektif { get; set; }
      public string NuKeg { get; set; }
      public string NmKegUnit { get; set; }
      public int LevelKeg { get; set; }
      public string Type { get; set; }
      public string KdKeg_Induk { get; set; }
      public DateTime? DateCreate { get; set; }
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
        var updated =
          await _context.MKegiatan.FindAsync(m => m.IdKeg == request.IdKeg);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.MKegiatan.Update(updated))
          throw new ApiException("Problem saving changes");

        return Unit.Value;
      }
    }
  }
}