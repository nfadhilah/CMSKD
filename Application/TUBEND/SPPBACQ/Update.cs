using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.SPPBACQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdSPP { get; set; }
      public long IdBerita { get; set; }

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
        RuleFor(d => d.IdSPP).NotEmpty();
        RuleFor(d => d.IdBerita).NotEmpty();
      }
    }

    public class Command : SPPBA, IRequest<SPPBADTO> { }

    public class Handler : IRequestHandler<Command, SPPBADTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPPBADTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.SPPBA.FindByIdAsync(request.IdSPPBA);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.SPPBA.Update(updated))
          throw new ApiException("Problem saving changes");

        var result = await
          _context.SPPBA.FindAllAsync<SPP, Berita>(
            x => x.IdSPPBA == updated.IdSPPBA, x => x.SPP,
            x => x.Berita);

        return _mapper.Map<SPPBADTO>(result.SingleOrDefault());
      }
    }
  }
}