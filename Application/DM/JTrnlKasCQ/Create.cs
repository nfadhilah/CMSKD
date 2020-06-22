using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.JTrnlKasCQ
{
  public class Create
  {
    public class Command : IRequest<JTrnlKas>
    {
      public int IdNoJeTra { get; set; }
      public string NmJeTra { get; set; }
      public string KdPers { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdNoJeTra).NotEmpty();
        RuleFor(d => d.NmJeTra).NotEmpty();
        RuleFor(d => d.KdPers).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, JTrnlKas>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JTrnlKas> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<JTrnlKas>(request);

        if (!await _context.JTrnlKas.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}