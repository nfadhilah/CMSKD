using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Program
{
  public class Create
  {
    public class Command : IRequest<MPgrm>
    {
      public long IdUnit { get; set; }
      public string NmPrgrm { get; set; }
      public string NuPrgrm { get; set; }
      public string IdPrioda { get; set; }
      public string IdPrioProv { get; set; }
      public string IdPrioNas { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdUnit).NotEmpty();
        RuleFor(d => d.NmPrgrm).NotEmpty();
        RuleFor(d => d.NuPrgrm).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, MPgrm>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<MPgrm> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<MPgrm>(request);

        if (!await _context.MPgrm.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}
