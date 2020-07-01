using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.BkBankCQ
{
  public class Create
  {
    public class Command : IRequest<BKBankDTO>
    {
      public long IdUnit { get; set; }
      public long IdBend { get; set; }
      public string NoBuku { get; set; }
      public string KdStatus { get; set; }
      public DateTime? TglBuku { get; set; }
      public string Uraian { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdUnit).NotEmpty();
        RuleFor(d => d.IdBend).NotEmpty();
        RuleFor(d => d.NoBuku).NotEmpty();
        RuleFor(d => d.KdStatus).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, BKBankDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BKBankDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<BkBank>(request);

        if (!await _context.BkBank.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.BkBank
          .FindAllAsync<DaftUnit, Bend, StatTrs>(x => x.IdBkBank == added.IdBkBank, x => x.Unit,
            x => x.Bend, x => x.StatTrs);

        return _mapper.Map<BKBankDTO>(result.SingleOrDefault());
      }
    }
  }
}