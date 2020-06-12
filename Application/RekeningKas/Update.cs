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

namespace Application.RekeningKas
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdUnit { get; set; }
      public long IdRek { get; set; }
      public long IdBank { get; set; }
      public string NmBKas { get; set; }
      public string NoRek { get; set; }
      public Decimal? Saldo { get; set; }

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
        RuleFor(d => d.IdRek).NotEmpty();
        RuleFor(d => d.IdBank).NotEmpty();
        RuleFor(d => d.NmBKas).NotEmpty();
        RuleFor(d => d.NoRek).NotEmpty();
        RuleFor(d => d.Saldo).NotEmpty();
      }
    }
    
    public class Command : IRequest
    {
      public long IdKas { get; set; }
      public long IdUnit { get; set; }
      public long IdRek { get; set; }
      public long IdBank { get; set; }
      public string NmBKas { get; set; }
      public string NoRek { get; set; }
      public Decimal? Saldo { get; set; }
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
          await _context.BkBKas.FindAsync(x => x.IdKas == request.IdKas);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.BkBKas.Update(updated))
          throw new ApiException("Problem saving changes");

        return Unit.Value;
      }
    }
  }
}