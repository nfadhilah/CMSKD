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

namespace Application.TUBEND.STSDetDCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdSTS { get; set; }
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

    public class Command : STSDetD, IRequest<STSDetDDTO>
    {
    }

    public class Handler : IRequestHandler<Command, STSDetDDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<STSDetDDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.STSDetD.FindByIdAsync(request.IdSTSDetD);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.STSDetD.Update(updated))
          throw new ApiException("Problem saving changes");

        var result = await _context.STSDetD
          .FindAllAsync<STS, DaftRekening>(
            x => x.IdSTSDetD == updated.IdSTSDetD,
            x => x.STS, x => x.Rekening);

        return _mapper.Map<STSDetDDTO>(result.SingleOrDefault());
      }
    }
  }
}