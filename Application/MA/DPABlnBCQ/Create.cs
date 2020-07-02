using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.MA;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MA.DPABlnBCQ
{
  public class Create
  {
    public class Command : IRequest<DPABlnBDTO>
    {
      public long IdDPAB { get; set; }
      public long IdBulan { get; set; }
      public Decimal? Nilai { get; set; }
      public DateTime? DateCreate { get; set; }
      public DateTime? DateUpdate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdDPAB).NotEmpty();
        RuleFor(d => d.IdBulan).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, DPABlnBDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DPABlnBDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<DPABlnB>(request);

        if (!await _context.DPABlnB.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.DPABlnB.FindAllAsync<DPAB>(x => x.IdDPABlnB == added.IdDPABlnB, x => x.DPAB);

        return _mapper.Map<DPABlnBDTO>(result.Single());
      }
    }
  }
}