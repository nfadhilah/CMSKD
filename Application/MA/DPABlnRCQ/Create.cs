using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.MA;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MA.DPABlnRCQ
{
  public class Create
  {
    public class Command : IRequest<DPABlnR>
    {
      public long IdDPAR { get; set; }
      public long IdBulan { get; set; }
      public Decimal? Nilai { get; set; }
      public DateTime? DateCreate { get; set; }
      public DateTime? DateUpdate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdDPAR).NotEmpty();
        RuleFor(d => d.IdBulan).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, DPABlnR>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DPABlnR> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<DPABlnR>(request);

        if (!await _context.DPABlnR.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}