using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.JBendCQ
{
  public class Create
  {
    public class Command : IRequest<JBend>
    {
      public string JnsBend { get; set; }
      public long IdRek { get; set; }
      public string UraiBend { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.JnsBend).NotEmpty();
        RuleFor(d => d.IdRek).NotEmpty();
        RuleFor(d => d.UraiBend).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, JBend>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JBend> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<JBend>(request);

        if (!await _context.JBend.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}