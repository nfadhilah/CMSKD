using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.SPPBPKCQ
{
  public class Create
  {
    public class Command : IRequest<SPPBPKDTO>
    {
      public long IdSPP { get; set; }
      public long IdBPK { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdSPP).NotEmpty();
        RuleFor(d => d.IdBPK).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, SPPBPKDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPPBPKDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<SPPBPK>(request);

        if (!await _context.SPPBPK.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.SPPBPK
          .FindAllAsync<SPP, BPK>(
            x => x.IdSPPBPK == added.IdSPPBPK, s => s.SPP, s => s.BPK);

        return _mapper.Map<SPPBPKDTO>(result.SingleOrDefault());
      }
    }
  }
}