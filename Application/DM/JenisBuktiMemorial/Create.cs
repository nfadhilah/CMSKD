using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.DM.JenisBuktiMemorial
{
  public class Create
  {
    public class Command : IRequest<JBM>
    {
      // public long IdJBM { get; set; }
      public string KdBM { get; set; }
      public string NmBM { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        // RuleFor(d => d.IdJBM).NotEmpty();
        RuleFor(d => d.KdBM).NotEmpty();
        RuleFor(d => d.NmBM).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, JBM>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JBM> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<JBM>(request);

        if (!await _context.JBM.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}