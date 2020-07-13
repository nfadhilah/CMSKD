using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.SPPBACQ
{
  public class Create
  {
    public class Command : IRequest<SPPBADTO>
    {
      public long IdSPP { get; set; }
      public long IdBerita { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdSPP).NotEmpty();
        RuleFor(d => d.IdBerita).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, SPPBADTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPPBADTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<SPPBA>(request);

        if (!await _context.SPPBA.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.SPPBA
          .FindAllAsync<SPP, Berita>(
            x => x.IdSPPBA == added.IdSPPBA, s => s.SPP, s => s.Berita);

        return _mapper.Map<SPPBADTO>(result.SingleOrDefault());
      }
    }
  }
}