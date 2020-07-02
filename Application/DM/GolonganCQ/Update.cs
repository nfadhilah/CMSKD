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

namespace Application.DM.GolonganCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public string KdGol { get; set; }
      public string NmGol { get; set; }
      public string Pangkat { get; set; }


      public DTO()
      {
        var config = new MapperConfiguration(opt => opt.CreateMap<DTO, Command>());

        _mapper = config.CreateMapper();
      }

      public Command MapDTO(Command destination) =>
        _mapper.Map(this, destination);
    }

    public class Validator : AbstractValidator<DTO>
    {
      public Validator()
      {
        RuleFor(d => d.KdGol).NotEmpty();
        RuleFor(d => d.NmGol).NotEmpty();
        RuleFor(d => d.Pangkat).NotEmpty();
      }
    }

    public class Command : Golongan, IRequest<GolonganDTO>
    {
    }

    public class Handler : IRequestHandler<Command, GolonganDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<GolonganDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.Golongan.FindAsync(x => x.IdGol == request.IdGol);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.Golongan.Update(updated))
          throw new ApiException("Problem saving changes");

        return _mapper.Map<GolonganDTO>(updated);
      }
    }
  }
}