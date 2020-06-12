using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using FluentValidation;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Program
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdUnit { get; set; }
      public string NmPrgrm { get; set; }
      public string NuPrgrm { get; set; }
      public string IdPrioda { get; set; }
      public string IdPrioProv { get; set; }
      public string IdPrioNas { get; set; }

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
        RuleFor(d => d.NmPrgrm).NotEmpty();
        RuleFor(d => d.NuPrgrm).NotEmpty();
      }
    }

    public class Command : IRequest
    {
      public long IdPrgrm { get; set; }
      public long IdUnit { get; set; }
      public string NmPrgrm { get; set; }
      public string NuPrgrm { get; set; }
      public string IdPrioda { get; set; }
      public string IdPrioProv { get; set; }
      public string IdPrioNas { get; set; }
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
          await _context.MPgrm.FindAsync(m => m.IdPrgrm == request.IdPrgrm);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.MPgrm.Update(updated))
          throw new ApiException("Problem saving changes");

        return Unit.Value;
      }
    }
  }
}