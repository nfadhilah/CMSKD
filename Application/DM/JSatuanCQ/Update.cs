using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.JSatuanCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public string KdSatuan { get; set; }
      public string UraiSatuan { get; set; }
      public string Ket { get; set; }

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
        RuleFor(d => d.KdSatuan).NotEmpty();
        RuleFor(d => d.UraiSatuan).NotEmpty();
        RuleFor(d => d.Ket).NotEmpty();
      }
    }

    public class Command : JSatuan, IRequest<JSatuan>
    {
    }

    public class Handler : IRequestHandler<Command, JSatuan>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JSatuan> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.JSatuan.FindAsync(x => x.IdSatuan == request.IdSatuan);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.JSatuan.Update(updated))
          throw new ApiException("Problem saving changes");

        return updated;
      }
    }
  }
}