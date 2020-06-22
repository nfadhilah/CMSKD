using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.BkBankDetCQ
{
  public class Create
  {
    public class Command : IRequest<BkBankDet>
    {
      public long IdBkBank { get; set; }
      public int NoJeTra { get; set; }
      public decimal? Nilai { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdBkBank).NotEmpty();
        RuleFor(d => d.NoJeTra).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, BkBankDet>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BkBankDet> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<BkBankDet>(request);

        if (!await _context.BkBankDet.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}