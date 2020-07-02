using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.StruRekCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public int MtgLevel { get; set; }
      public string NmLevel { get; set; }

      public DTO()
      {
        var config = new MapperConfiguration(opt => opt.CreateMap<DTO, Command>());

        _mapper = config.CreateMapper();
      }

      public Command MapDTO(Command destination) =>
        _mapper.Map(this, destination);
    }

    public class Validator : AbstractValidator<DTO>
    {
      public Validator()
      {
        RuleFor(d => d.MtgLevel).NotEmpty();
        RuleFor(d => d.NmLevel).NotEmpty();
      }
    }

    public class Command : StruRek, IRequest<StruRek>
    {
    }

    public class Handler : IRequestHandler<Command, StruRek>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<StruRek> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.StruRek.FindAsync(x => x.IdStruRek == request.IdStruRek);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.StruRek.Update(updated))
          throw new ApiException("Problem saving changes");

        return updated;
      }
    }
  }
}
