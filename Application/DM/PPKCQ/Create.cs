using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.PPKCQ
{
  public class Create
  {
    public class Command : IRequest<PPKDTO>
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

    public class Handler : IRequestHandler<Command, PPKDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<PPKDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<PPK>(request);

        if (!await _context.PPK.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result =
          await _context.PPK.FindAllAsync<Pegawai>(x => x.IdPPK == added.IdPPK,
            x => x.Pegawai);

        return _mapper.Map<PPKDTO>(result.Single());
      }
    }
  }
}