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

namespace Application.MA.DPABCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdDPA { get; set; }
      public int? IdXKode { get; set; }
      public string KdTahap { get; set; }
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
        RuleFor(d => d.IdRek).NotEmpty();
        RuleFor(d => d.KdTahap).NotEmpty();
        RuleFor(d => d.IdXKode).NotEmpty();
      }
    }

    public class Command : DPAB, IRequest<DPABDTO> { }

    public class Handler : IRequestHandler<Command, DPABDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DPABDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.DPAB.FindByIdAsync(request.IdDPAB);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.DPAB.Update(updated))
          throw new ApiException("Problem saving changes");

        var result =
          await _context.DPAB.FindAllAsync<DPA, DaftRekening>(
            x => x.IdDPAB == updated.IdDPAB, x => x.DPA, x => x.DaftRekening);

        return _mapper.Map<DPABDTO>(result.Single());
      }
    }
  }
}