using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.TBPLDetKegCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdTBPLDet { get; set; }
      public int IdNoJeTra { get; set; }
      public long IdKeg { get; set; }
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
        RuleFor(d => d.IdTBPLDet).NotEmpty();
        RuleFor(d => d.IdNoJeTra).NotEmpty();
        RuleFor(d => d.IdKeg).NotEmpty();
      }
    }

    public class Command : TBPLDetKeg, IRequest
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
        var updated =
          await _context.TBPLDetKeg.FindByIdAsync(request.IdTBPLDetKeg);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.TBPLDetKeg.Update(updated))
          throw new ApiException("Problem saving changes");

        return Unit.Value;
      }
    }
  }
}