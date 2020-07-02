using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.DaftFungsiCQ
{
  public class Create
  {
    public class Command : IRequest<DaftFungsiDTO>
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

    public class Handler : IRequestHandler<Command, DaftFungsiDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DaftFungsiDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<DaftFungsi>(request);

        if (!await _context.DaftFungsi.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return _mapper.Map<DaftFungsiDTO>(added);
      }
    }
  }
}