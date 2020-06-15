using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Bendahara
{

  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public string IdUnit { get; set; }
      public string IdPeg { get; set; }
      public string Jns_Bend { get; set; }
      public int StAktif { get; set; }
      public string IdBank { get; set; }
      public string Jab_Bend { get; set; }
      public string RekBend { get; set; }
      public string NPWPBend { get; set; }
      public Decimal? SaldoBend { get; set; }
      public Decimal? SaldoBendT { get; set; }
      public DateTime? TglStopBend { get; set; }
      public DateTime? DateCreate { get; set; }

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
        RuleFor(d => d.IdPeg).NotEmpty();
        RuleFor(d => d.Jns_Bend).NotEmpty();
        RuleFor(d => d.RekBend).NotEmpty();
        RuleFor(d => d.IdBank).NotEmpty();
        RuleFor(d => d.Jab_Bend).NotEmpty();
      }
    }

    public class Command : IRequest
    {
      public string IdBend { get; set; }
      public string IdUnit { get; set; }
      public string IdPeg { get; set; }
      public string Jns_Bend { get; set; }
      public int StAktif { get; set; }
      public string IdBank { get; set; }
      public string Jab_Bend { get; set; }
      public string RekBend { get; set; }
      public string NPWPBend { get; set; }
      public Decimal? SaldoBend { get; set; }
      public Decimal? SaldoBendT { get; set; }
      public DateTime? TglStopBend { get; set; }
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
          await _context.Bend.FindByIdAsync(request.IdBend);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.Bend.Update(updated))
          throw new ApiException("Problem saving changes");

        return Unit.Value;
      }
    }
  }
}