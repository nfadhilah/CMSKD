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

namespace Application.TUBEND.SPPBPKCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdSPP { get; set; }
      public long IdBPK { get; set; }

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
        RuleFor(d => d.IdBPK).NotEmpty();
      }
    }

    public class Command : SPPBPK, IRequest<SPPBPKDTO> { }

    public class Handler : IRequestHandler<Command, SPPBPKDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPPBPKDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.SPPBPK.FindByIdAsync(request.IdSPPBPK);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.SPPBPK.Update(updated))
          throw new ApiException("Problem saving changes");

        var result = await
          _context.SPPBPK.FindAllAsync<SPP, BPK>(
            x => x.IdSPPBPK == updated.IdSPPBPK, x => x.SPP,
            x => x.BPK);

        return _mapper.Map<SPPBPKDTO>(result.SingleOrDefault());
      }
    }
  }
}