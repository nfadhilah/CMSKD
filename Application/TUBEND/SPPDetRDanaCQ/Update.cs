using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.SPPDetRDanaCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdSPPDetR { get; set; }
      public long IdJDana { get; set; }
      public decimal? Nilai { get; set; }

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
        RuleFor(d => d.IdSPPDetR).NotEmpty();
        RuleFor(d => d.IdJDana).NotEmpty();
      }
    }

    public class Command : SPPDetRDana, IRequest<SPPDetRDanaDTO>
    {
    }

    public class Handler : IRequestHandler<Command, SPPDetRDanaDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPPDetRDanaDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.SPPDetRDana.FindByIdAsync(request.IdSPPDetRDana);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.SPPDetRDana.Update(updated))
          throw new ApiException("Problem saving changes");

        var result = await _context.SPPDetRDana
          .FindAllAsync<SPPDetR, JDana>(
            x => x.IdSPPDetRDana == updated.IdSPPDetRDana, x => x.SPPDetR,
            x => x.JDana);

        return _mapper.Map<SPPDetRDanaDTO>(result.SingleOrDefault());
      }
    }
  }
}