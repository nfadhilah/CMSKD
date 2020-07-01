using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.BkBankDetCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdBkBank { get; set; }
      public int IdNoJeTra { get; set; }
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
        RuleFor(d => d.IdBkBank).NotEmpty();
        RuleFor(d => d.IdNoJeTra).NotEmpty();
      }
    }

    public class Command : BkBankDet, IRequest<BKBankDetDTO> { }

    public class Handler : IRequestHandler<Command, BKBankDetDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BKBankDetDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.BkBankDet.FindByIdAsync(request.IdBankDet);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.BkBankDet.Update(updated))
          throw new ApiException("Problem saving changes");

        var result = await _context.BkBankDet
          .FindAllAsync<BkBank, JTrnlKas>(x => x.IdBankDet == updated.IdBankDet,
            x => x.BkBank,
            x => x.JTrnlKas);

        return _mapper.Map<BKBankDetDTO>(result);
      }
    }
  }
}