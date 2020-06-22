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

namespace Application.TUBEND.BkPajakDetStrCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdBkPajakStr { get; set; }
      public long? IdPajak { get; set; }
      public long IdBkPajak { get; set; }
      public string IdBilling { get; set; }
      public DateTime? TglIdBilling { get; set; }
      public DateTime? TglExpire { get; set; }
      public string NTPN { get; set; }
      public string NTB { get; set; }

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
        RuleFor(d => d.IdBkPajakStr).NotEmpty();
        RuleFor(d => d.IdPajak).NotEmpty();
        RuleFor(d => d.IdBkPajak).NotEmpty();
      }
    }

    public class Command : BkPajakDetStr, IRequest
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
          await _context.BkPajakDetStr.FindByIdAsync(request.IdBkPajakDetStr);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.BkPajakDetStr.Update(updated))
          throw new ApiException("Problem saving changes");

        return Unit.Value;
      }
    }
  }
}