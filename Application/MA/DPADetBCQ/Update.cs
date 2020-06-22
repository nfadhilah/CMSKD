using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.MA;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.MA.DPADetBCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdDPAB { get; set; }
      public string KdNilai { get; set; }
      public string KdJabar { get; set; }
      public string Uraian { get; set; }
      public Decimal? JumBYek { get; set; }
      public string Satuan { get; set; }
      public Decimal? Tarif { get; set; }
      public Decimal? SubTotal { get; set; }
      public string Ekspresi { get; set; }
      public byte InclSubtotal { get; set; }
      public string Type { get; set; }
      public string IdStdHarga { get; set; }

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
        RuleFor(d => d.IdDPAB).NotEmpty();
        RuleFor(d => d.KdNilai).NotEmpty();
      }
    }

    public class Command : DPADetB, IRequest
    {
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
          await _context.DPADetB.FindByIdAsync(request.IdDPADetB);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.DPADetB.Update(updated))
          throw new ApiException("Problem saving changes");

        return Unit.Value;
      }
    }
  }
}