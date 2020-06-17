using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.JBKasCQ
{
  public class Create
  {
    public class Command : IRequest<JBKas>
    {
      public string NmBKas { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.NmBKas).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, JBKas>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JBKas> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<JBKas>(request);

        if (!await _context.JBKas.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}