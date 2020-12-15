using System.Linq;
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
    public class Command : IRequest<NrcBendDTO>
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

    public class Handler : IRequestHandler<Command, NrcBendDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<NrcBendDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<NrcBend>(request);

        if (!await _context.NrcBend.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result =
          await _context.NrcBend.FindAllAsync<DaftRekening>(
            x => x.IdNrcBend == added.IdNrcBend, x => x.DaftRekening);

        return _mapper.Map<NrcBendDTO>(result.Single());
      }
    }
  }
}