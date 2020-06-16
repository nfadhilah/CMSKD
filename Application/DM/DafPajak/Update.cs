using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.DM.DafPajak
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public string KdPajak { get; set; }
      public string NmPajak { get; set; }
      public string Uraian { get; set; }
      public string Keterangan { get; set; }
      public string RumusPajak { get; set; }
      public int StAktif { get; set; }
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
        RuleFor(d => d.KdPajak).NotEmpty();
        RuleFor(d => d.NmPajak).NotEmpty();
        RuleFor(d => d.Uraian).NotEmpty();
        RuleFor(d => d.Keterangan).NotEmpty();
        RuleFor(d => d.RumusPajak).NotEmpty();
        RuleFor(d => d.StAktif).NotEmpty();
        RuleFor(d => d.DateCreate).NotEmpty();
      }
    }

    public class Command : IRequest
    {
      public long IdPjk { get; set; }
      public string KdPajak { get; set; }
      public string NmPajak { get; set; }
      public string Uraian { get; set; }
      public string Keterangan { get; set; }
      public string RumusPajak { get; set; }
      public int StAktif { get; set; }
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
          await _context.Pajak.FindByIdAsync(request.IdPjk);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.Pajak.Update(updated))
          throw new ApiException("Problem saving changes");

        return Unit.Value;
      }
    }
  }
}