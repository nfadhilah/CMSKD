using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.BendKPACQ
{
  public class Create
  {
    public class Command : IRequest<BendKPADTO>
    {
      public long IdBend { get; set; }
      public long IdPeg { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdBend).NotEmpty();
        RuleFor(d => d.IdPeg).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, BendKPADTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BendKPADTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<BendKPA>(request);

        if (!await _context.BendKPA.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.BendKPA.FindAllAsync<Bend, Pegawai>(
          x => x.IdBendKPA == added.IdBendKPA, x => x.Bend, x => x.Pegawai);

        return _mapper.Map<BendKPADTO>(result);
      }
    }
  }
}