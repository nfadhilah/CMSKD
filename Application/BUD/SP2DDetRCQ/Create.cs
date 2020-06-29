using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.BUD;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BUD.SP2DDetRCQ
{
  public class Create
  {
    public class Command : IRequest<SP2DDetR>
    {
      public long IdSP2D { get; set; }
      public long IdKeg { get; set; }
      public long IdRek { get; set; }
      public int IdNoJeTra { get; set; }
      public decimal? Nilai { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdSP2D).NotEmpty();
        RuleFor(d => d.IdKeg).NotEmpty();
        RuleFor(d => d.IdRek).NotEmpty();
        RuleFor(d => d.IdNoJeTra).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, SP2DDetR>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SP2DDetR> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<SP2DDetR>(request);

        if (!await _context.SP2DDetR.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.SP2DDetR
          .FindAllAsync<SP2D, MKegiatan, DaftRekening>(
            x => x.IdSP2DDetR == added.IdSP2DDetR, x => x.SP2D, x => x.Kegiatan,
            x => x.Rekening);

        return result.SingleOrDefault();
      }
    }
  }
}