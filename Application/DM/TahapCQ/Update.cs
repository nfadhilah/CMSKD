using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.TahapCQ
{

  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public string Uraian { get; set; }
      public DateTime? TglTransfer { get; set; }

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
        RuleFor(d => d.Uraian).NotEmpty();
      }
    }

    public class Command : Tahap, IRequest<Tahap>
    {
    }

    public class Handler : IRequestHandler<Command, Tahap>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<Tahap> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.Tahap.FindByIdAsync(request.KdTahap);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.Tahap.Update(updated))
          throw new ApiException("Problem saving changes");

        return updated;
      }
    }
  }
}