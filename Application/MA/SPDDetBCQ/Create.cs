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

namespace Application.MA.SPDDetBCQ
{
  public class Create
  {
    public class Command : IRequest<SPDDetBDTO>
    {
      public long IdSPD { get; set; }
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
      }
    }

    public class Handler : IRequestHandler<Command, SPDDetBDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPDDetBDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<SPDDetB>(request);

        if (!await _context.SPDDetB.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.SPDDetB.FindAllAsync<SPD, DaftRekening>(
          x => x.IdSPDDetB == added.IdSPDDetB, x => x.SPD, x => x.Rekening);

        return _mapper.Map<SPDDetBDTO>(result.Single());
      }
    }
  }
}