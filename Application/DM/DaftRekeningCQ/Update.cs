using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.DM.DaftRekeningCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public string KdPer { get; set; }
      public string NmPer { get; set; }
      public int MtgLevel { get; set; }
      public int KdKhusus { get; set; }
      public long IdJRek { get; set; }
      public long? IdJnsAkun { get; set; }
      public string Type { get; set; }
      public int? StAktif { get; set; }

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
        RuleFor(d => d.KdPer).NotEmpty();
        RuleFor(d => d.NmPer).NotEmpty();
        RuleFor(d => d.MtgLevel).NotEmpty();
        RuleFor(d => d.KdKhusus).NotEmpty();
        RuleFor(d => d.IdJRek).NotEmpty();
        RuleFor(d => d.Type).NotEmpty();
      }
    }

    public class Command : IRequest
    {
      public long IdRek { get; set; }
      public string KdPer { get; set; }
      public string NmPer { get; set; }
      public int MtgLevel { get; set; }
      public int KdKhusus { get; set; }
      public long IdJRek { get; set; }
      public long? IdJnsAkun { get; set; }
      public string Type { get; set; }
      public int? StAktif { get; set; }
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
          await _context.DaftRekening.FindByIdAsync(request.IdRek);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.DaftRekening.Update(updated))
          throw new ApiException("Problem saving changes");

        return Unit.Value;
      }
    }
  }
}