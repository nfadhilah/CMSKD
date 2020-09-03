using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.DM.MetodePengadaanCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public string Kode { get; set; }
      public string Uraian { get; set; }

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
        RuleFor(d => d.Kode).NotEmpty();
        RuleFor(d => d.Uraian).NotEmpty();
      }
    }

    public class Command : MetodePengadaan, IRequest<MetodePengadaan> { }

    public class Handler : IRequestHandler<Command, MetodePengadaan>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<MetodePengadaan> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.MetodePengadaan.FindByIdAsync(
            request.IdMetodePengadaan);

        if (updated == null)
          throw new ApiException("Not found", (int) HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.MetodePengadaan.Update(updated))
          throw new ApiException("Problem saving changes");

        return updated;
      }
    }
  }
}