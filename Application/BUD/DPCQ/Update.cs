using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.BUD;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BUD.DPCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public string NoDP { get; set; }
      public int? IdxKode { get; set; }
      public long? IdTtd { get; set; }
      public DateTime? TglDP { get; set; }
      public string Uraian { get; set; }
      public DateTime? TglValid { get; set; }

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
        RuleFor(d => d.NoDP).NotEmpty();
      }
    }

    public class Command : DP, IRequest<DPDTO>
    {
    }

    public class Handler : IRequestHandler<Command, DPDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DPDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.DP.FindByIdAsync(request.IdDP);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.DP.Update(updated))
          throw new ApiException("Problem saving changes");

        return _mapper.Map<DPDTO>(updated);
      }
    }
  }
}