using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.MA;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MA.SPDDetBCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdSPD { get; set; }
      public long IdKeg { get; set; }
      public long IdRek { get; set; }
      public decimal? Nilai { get; set; }
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
        RuleFor(d => d.IdSPD).NotEmpty();
        RuleFor(d => d.IdRek).NotEmpty();
        RuleFor(d => d.IdKeg).NotEmpty();
      }
    }

    public class Command : SPDDetB, IRequest<SPDDetBDTO> { }

    public class Handler : IRequestHandler<Command, SPDDetBDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPDDetBDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.SPDDetB.FindByIdAsync(request.IdSPDDetB);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.SPDDetB.Update(updated))
          throw new ApiException("Problem saving changes");

        var result = await _context.SPDDetB.FindAllAsync<SPD, DaftRekening>(
          x => x.IdSPDDetB == updated.IdSPDDetB, x => x.SPD, x => x.Rekening);

        return _mapper.Map<SPDDetBDTO>(result.Single());
      }
    }
  }
}