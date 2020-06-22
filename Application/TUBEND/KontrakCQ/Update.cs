using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.KontrakCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdUnit { get; set; }
      public string NoKontrak { get; set; }
      public long IdPhk3 { get; set; }
      public long IdKeg { get; set; }
      public DateTime? TglKon { get; set; }
      public DateTime? TglAwalKontrak { get; set; }
      public DateTime? TglAkhirKontrak { get; set; }
      public DateTime? TglSlsKontrak { get; set; }
      public string Uraian { get; set; }
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
        RuleFor(d => d.IdUnit).NotEmpty();
        RuleFor(d => d.NoKontrak).NotEmpty();
        RuleFor(d => d.IdPhk3).NotEmpty();
        RuleFor(d => d.IdKeg).NotEmpty();
      }
    }

    public class Command : Kontrak, IRequest
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
          await _context.Kontrak.FindByIdAsync(request.IdKontrak);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.Kontrak.Update(updated))
          throw new ApiException("Problem saving changes");

        return Unit.Value;
      }
    }
  }
}