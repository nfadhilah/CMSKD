using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.DM.BendaharaKPA
{
  public class Create
  {
    public class Command : IRequest<BendKPA>
    {
      public long IdUnit { get; set; }
      public long IdBend { get; set; }
      public long IdPeg { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdUnit).NotEmpty();
        RuleFor(d => d.IdBend).NotEmpty();
        RuleFor(d => d.IdPeg).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, BendKPA>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BendKPA> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<BendKPA>(request);

        if (!await _context.BendKPA.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}