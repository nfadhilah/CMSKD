using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.BendCQ
{

  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public string JnsBend { get; set; }
      public long IdPeg { get; set; }
      public string IdBank { get; set; }
      public string NmCabBank { get; set; }
      public string RekBend { get; set; }
      public string NPWPBend { get; set; }
      public string JabBend { get; set; }
      public Decimal? SaldoBend { get; set; }
      public Decimal? SaldoBendT { get; set; }
      public DateTime? TglStopBend { get; set; }
      public string WargaNegara { get; set; }
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
        RuleFor(d => d.IdPeg).NotEmpty();
        RuleFor(d => d.JnsBend).NotEmpty();
        RuleFor(d => d.RekBend).NotEmpty();
        RuleFor(d => d.IdBank).NotEmpty();
        RuleFor(d => d.JabBend).NotEmpty();
        RuleFor(d => d.NPWPBend).NotEmpty();
      }
    }

    public class Command : Bend, IRequest<BendDTO>
    {
    }

    public class Handler : IRequestHandler<Command, BendDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BendDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.Bend.FindByIdAsync(request.IdBend);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.Bend.Update(updated))
          throw new ApiException("Problem saving changes");

        var result = await _context.Bend.FindAllAsync<Pegawai, JBank>(
          x => x.IdBend == updated.IdBend, x => x.Peg, x => x.Bank);

        return _mapper.Map<BendDTO>(result);
      }
    }
  }
}