using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.BkBankDetCQ
{
  public class Create
  {
    public class Command : IRequest<BKBankDetDTO>
    {
      public long IdBkBank { get; set; }
      public int IdNoJeTra { get; set; }
      public decimal? Nilai { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdBkBank).NotEmpty();
        RuleFor(d => d.IdNoJeTra).NotEmpty();
      }
    }

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
        var added = _mapper.Map<BkBankDet>(request);

        if (!await _context.BkBankDet.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.BkBankDet
          .FindAllAsync<BkBank, JTrnlKas>(x => x.IdBankDet == added.IdBankDet,
            x => x.BkBank,
            x => x.JTrnlKas);

        return _mapper.Map<BKBankDetDTO>(result.SingleOrDefault());
      }
    }
  }
}