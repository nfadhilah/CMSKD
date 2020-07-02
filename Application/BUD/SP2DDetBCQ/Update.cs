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

namespace Application.BUD.SP2DDetBCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdSP2D { get; set; }
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
        RuleFor(d => d.IdRek).NotEmpty();
        RuleFor(d => d.IdNoJeTra).NotEmpty();
      }
    }

    public class Command : SP2DDetB, IRequest<SP2DDetBDTO>
    {
    }

    public class Handler : IRequestHandler<Command, SP2DDetBDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SP2DDetBDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.SP2DDetB.FindByIdAsync(request.IdSP2DDetB);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.SP2DDetB.Update(updated))
          throw new ApiException("Problem saving changes");

        var result = await _context.SP2DDetB
          .FindAllAsync<SP2D, DaftRekening>(
            x => x.IdSP2DDetB == updated.IdSP2DDetB, x => x.SP2D,
            x => x.Rekening);

        return _mapper.Map<SP2DDetBDTO>(result.SingleOrDefault());
      }
    }
  }
}