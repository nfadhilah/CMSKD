using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.MA;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MA.DPARCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdDPA { get; set; }
      public string KdTahap { get; set; }
      public int IdXKode { get; set; }
      public long IdKeg { get; set; }
      public long IdRek { get; set; }
      public Decimal? Nilai { get; set; }

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
        RuleFor(d => d.IdDPA).NotEmpty();
        RuleFor(d => d.KdTahap).NotEmpty();
        RuleFor(d => d.IdXKode).NotEmpty();
        RuleFor(d => d.IdKeg).NotEmpty();
        RuleFor(d => d.IdRek).NotEmpty();
      }
    }

    public class Command : DPAR, IRequest<DPARDTO>
    {
    }

    public class Handler : IRequestHandler<Command, DPARDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DPARDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.DPAR.FindByIdAsync(request.IdDPAR);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.DPAR.Update(updated))
          throw new ApiException("Problem saving changes");

        var result =
          await _context.DPAR.FindAllAsync<DPA, MKegiatan, DaftRekening>(
            x => x.IdDPAR == updated.IdDPAR, x => x.DPA, x => x.Kegiatan,
            x => x.DaftRekening);

        return _mapper.Map<DPARDTO>(result);
      }
    }
  }
}