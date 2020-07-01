using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.BKPajakCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdUnit { get; set; }
      public long IdBend { get; set; }
      public string NoBkPajak { get; set; }
      public int IdxKode { get; set; }
      public string KdStatus { get; set; }
      public DateTime? TglBkPajak { get; set; }
      public string Uraian { get; set; }
      public DateTime? TglValid { get; set; }
      public long? KdRilis { get; set; }
      public int? StKirim { get; set; }
      public int? StCair { get; set; }
      public int IdTransfer { get; set; }

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
        RuleFor(d => d.IdUnit).NotEmpty();
        RuleFor(d => d.IdBend).NotEmpty();
        RuleFor(d => d.NoBkPajak).NotEmpty();
        RuleFor(d => d.KdStatus).NotEmpty();
        RuleFor(d => d.IdxKode).NotEmpty();
      }
    }

    public class Command : BkPajak, IRequest<BkPajakDTO> { }

    public class Handler : IRequestHandler<Command, BkPajakDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BkPajakDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.BkPajak.FindByIdAsync(request.IdBkPajak);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.BkPajak.Update(updated))
          throw new ApiException("Problem saving changes");

        var result = await _context.BkPajak
          .FindAllAsync<DaftUnit, Bend>(
            x => x.IdBkPajak == updated.IdBkPajak, x => x.Unit, x => x.Bend);

        return _mapper.Map<BkPajakDTO>(result);
      }
    }
  }
}