using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.ZKodeCQ
{
  public class Create
  {
    public class Command : IRequest<ZKode>
    {
      public int IdxKode { get; set; }
      public string Uraian { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(x => x.IdxKode).NotEmpty();
        RuleFor(x => x.Uraian).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, ZKode>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<ZKode> Handle(Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<ZKode>(request);

        if (!await _context.ZKode.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}
