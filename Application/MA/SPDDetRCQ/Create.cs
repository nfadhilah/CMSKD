using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.MA;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MA.SPDDetRCQ
{
  public class Create
  {
    public class Command : IRequest<SPDDetRDTO>
    {
      public long IdSPD { get; set; }
      public long IdKeg { get; set; }
      public long IdRek { get; set; }
      public decimal? Nilai { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdSPD).NotEmpty();
        RuleFor(d => d.IdRek).NotEmpty();
        RuleFor(d => d.IdKeg).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, SPDDetRDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPDDetRDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<SPDDetR>(request);

        if (!await _context.SPDDetR.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await
          _context.SPDDetR.FindAllAsync<SPD, MKegiatan, DaftRekening>(
            x => x.IdSPDDetR == added.IdSPDDetR, x => x.SPD, x => x.Kegiatan,
            x => x.Rekening);

        return _mapper.Map<SPDDetRDTO>(result.Single());
      }
    }
  }
}