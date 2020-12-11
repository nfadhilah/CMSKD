using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TU;
using FluentValidation;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TU.RkmDetDCQ
{
  public class Create
  {
    public class Command : IRequest<RkmDetD>
    {
      public string MtgKey { get; set; }
      public string UnitKey { get; set; }
      public string NoSTS { get; set; }
      public string NoJeTra { get; set; }
      public decimal? Nilai { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(x => x.MtgKey).NotEmpty();
        RuleFor(x => x.UnitKey).NotEmpty();
        RuleFor(x => x.NoSTS).NotEmpty();
        RuleFor(x => x.NoJeTra).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, RkmDetD>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<RkmDetD> Handle(Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<RkmDetD>(request);

        if (!await _context.RkmDetD.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.RkmDetD.FindAsync(x =>
          x.UnitKey == added.UnitKey && x.NoSTS == added.NoSTS && x.MtgKey == added.MtgKey &&
          x.NoJeTra == added.NoJeTra);

        return result;
      }
    }
  }
}