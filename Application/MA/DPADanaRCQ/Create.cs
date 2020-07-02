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

namespace Application.MA.DPADanaRCQ
{
  public class Create
  {
    public class Command : IRequest<DPADanaRDTO>
    {
      public long IdDPAR { get; set; }
      public long IdJDana { get; set; }
      public Decimal? Nilai { get; set; }
      public DateTime? DateCreate { get; set; }
      public DateTime? DateUpdate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdDPAR).NotEmpty();
        RuleFor(d => d.IdJDana).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, DPADanaRDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DPADanaRDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<DPADanaR>(request);

        if (!await _context.DPADanaR.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.DPADanaR.FindAllAsync<DPAR, JDana>(
          x => x.IdDPADanaR == added.IdDPADanaR, x => x.DPAR, x => x.JDana);

        return _mapper.Map<DPADanaRDTO>(result.Single());
      }
    }
  }
}