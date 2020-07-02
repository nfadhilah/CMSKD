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

namespace Application.MA.DPADanaBCQ
{
  public class Create
  {
    public class Command : IRequest<DPADanaBDTO>
    {
      public long IdDPAB { get; set; }
      public long IdJDana { get; set; }
      public Decimal? Nilai { get; set; }
      public DateTime? DateCreate { get; set; }
      public DateTime? DateUpdate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdDPAB).NotEmpty();
        RuleFor(d => d.IdJDana).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, DPADanaBDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DPADanaBDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<DPADanaB>(request);

        if (!await _context.DPADanaB.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.DPADanaB.FindAllAsync<DPAB, JDana>(
          x => x.IdDPADanaB == added.IdDPADanaB, x => x.DPAB, x => x.JDana);

        return _mapper.Map<DPADanaBDTO>(result.Single());
      }
    }
  }
}