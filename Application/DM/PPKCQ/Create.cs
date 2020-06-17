using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.PPKCQ
{
  public class Create
  {
    public class Command : IRequest<PPK>
    {
      public long IdPeg { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdPeg).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, PPK>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<PPK> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<PPK>(request);

        if (!await _context.PPK.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}