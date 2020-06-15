using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.PenggunaAnggaran
{
  public class Create
  {
    public class Command : IRequest<PA>
    {
      public long IdPeg { get; set; }
      public long IdUnit { get; set; }
      public string NIP { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdPeg).NotEmpty();
        RuleFor(d => d.IdUnit).NotEmpty();
        RuleFor(d => d.NIP).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, PA>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<PA> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<PA>(request);

        if (!await _context.PA.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}