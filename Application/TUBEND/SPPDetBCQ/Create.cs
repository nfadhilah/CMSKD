using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.SPPDetBCQ
{
  public class Create
  {
    public class Command : IRequest<SPPDetB>
    {
      public long IdRek { get; set; }
      public long IdSPP { get; set; }
      public int IdNoJeTra { get; set; }
      public decimal? Nilai { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdRek).NotEmpty();
        RuleFor(d => d.IdSPP).NotEmpty();
        RuleFor(d => d.IdNoJeTra).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, SPPDetB>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPPDetB> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<SPPDetB>(request);

        if (!await _context.SPPDetB.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.SPPDetB
          .FindAllAsync<DaftRekening, SPP, JTrnlKas>(
            x => x.IdSPPDetB == added.IdSPPDetB, x => x.Rekening, x => x.SPP,
            x => x.JTrnlKas);

        return result.SingleOrDefault();
      }
    }
  }
}