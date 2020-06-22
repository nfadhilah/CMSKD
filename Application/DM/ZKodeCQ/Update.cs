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

namespace Application.DM.ZKodeCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public string Uraian { get; set; }

      public DTO()
      {
        var config = new MapperConfiguration(opt =>
        {
          opt.CreateMap<DTO, Command>();
        });

        _mapper = config.CreateMapper();
      }

      public Command MapDTO(Command destination) =>
        _mapper.Map(this, destination);
    }

    public class Validator : AbstractValidator<DTO>
    {
      public Validator()
      {
        RuleFor(x => x.Uraian).NotEmpty();
      }
    }

    public class Command : ZKode, IRequest
    {
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
        var updated = await _context.ZKode.FindByIdAsync(request.IdxKode);

        if (updated == null) throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.ZKode.Update(updated))
          throw new ApiException("Problem saving changes");

        return Unit.Value;
      }
    }
  }
}