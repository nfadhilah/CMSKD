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

namespace Application.TUBEND.SPPDetRCQ
{
  public class Create
  {
    public class Command : IRequest<SPPDetRDTO>
    {
      public long IdRek { get; set; }
      public long IdKeg { get; set; }
      public long IdSPP { get; set; }
      public int IdNoJeTra { get; set; }
      public decimal? Nilai { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdKeg).NotEmpty();
        RuleFor(d => d.IdRek).NotEmpty();
        RuleFor(d => d.IdSPP).NotEmpty();
        RuleFor(d => d.IdNoJeTra).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, SPPDetRDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPPDetRDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<SPPDetR>(request);

        if (!await _context.SPPDetR.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.SPPDetR
          .FindAllAsync<DaftRekening, MKegiatan, SPP, JTrnlKas>(
            x => x.IdSPPDetR == added.IdSPPDetR, x => x.Rekening,
            x => x.Kegiatan, x => x.SPP, x => x.JTrnlKas);

        return _mapper.Map<SPPDetRDTO>(result.SingleOrDefault());
      }
    }
  }
}