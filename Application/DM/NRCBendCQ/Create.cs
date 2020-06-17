using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.DM.NRCBendCQ
{
  public class Create
  {
    public class Command : IRequest<NrcBend>
    {
      public long IdBend { get; set; }
      public long IdRek { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdBend).NotEmpty();
        RuleFor(d => d.IdRek).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, NrcBend>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<NrcBend> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<NrcBend>(request);

        if (!await _context.NrcBend.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}