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

namespace Application.DM.NrcBendCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdBend { get; set; }
      public long IdRek { get; set; }

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
        RuleFor(d => d.IdBend).NotEmpty();
        RuleFor(d => d.IdRek).NotEmpty();
      }
    }

    public class Command : NrcBend, IRequest<NrcBendDTO>
    {
    }

    public class Handler : IRequestHandler<Command, NrcBendDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<NrcBendDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.NrcBend.FindByIdAsync(request.IdNrcBend);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.NrcBend.Update(updated))
          throw new ApiException("Problem saving changes");

        var result =
          await _context.NrcBend.FindAllAsync<DaftRekening>(
            x => x.IdNrcBend == updated.IdNrcBend, x => x.DaftRekening);

        return _mapper.Map<NrcBendDTO>(result.Single());
      }
    }
  }
}