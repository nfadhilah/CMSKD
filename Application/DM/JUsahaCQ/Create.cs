using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.JUsahaCQ
{
  public class Create
  {
    public class Command : IRequest<JUsaha>
    {
      public string BadanUsaha { get; set; }
      public string Keterangan { get; set; }
      public string Akronim { get; set; }
    }

    public class Validator : AbstractValidator<JUsaha>
    {
      public Validator()
      {
        RuleFor(d => d.BadanUsaha).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, JUsaha>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JUsaha> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<JUsaha>(request);

        if (!await _context.JUsaha.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}
