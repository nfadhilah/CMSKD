using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.DM.DaftFungsiCQ
{
  public class Create
  {
    public class Command : IRequest<DaftFungsi>
    {
      // public long IdFung { get; set; }
      public string KdFung { get; set; }
      public string NmFung { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        // RuleFor(d => d.IdFung).NotEmpty();
        RuleFor(d => d.KdFung).NotEmpty();
        RuleFor(d => d.NmFung).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, DaftFungsi>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DaftFungsi> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<DaftFungsi>(request);

        if (!await _context.DaftFungsi.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}