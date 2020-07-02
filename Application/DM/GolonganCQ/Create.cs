using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.GolonganCQ
{
  public class Create
  {
    public class Command : IRequest<GolonganDTO>
    {
      public string KdGol { get; set; }
      public string NmGol { get; set; }
      public string Pangkat { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.KdGol).NotEmpty();
        RuleFor(d => d.NmGol).NotEmpty();
        RuleFor(d => d.Pangkat).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, GolonganDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<GolonganDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<Golongan>(request);

        if (!await _context.Golongan.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return _mapper.Map<GolonganDTO>(added);
      }
    }
  }
}
