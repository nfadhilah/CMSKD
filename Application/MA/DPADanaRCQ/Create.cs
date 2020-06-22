using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.MA;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MA.DPADanaRCQ
{
  public class Create
  {
    public class Command : IRequest<DPADanaR>
    {
      public long IdDPAR { get; set; }
      public string KdDana { get; set; }
      public Decimal? Nilai { get; set; }
      public DateTime? DateCreate { get; set; }
      public DateTime? DateUpdate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdDPAR).NotEmpty();
        RuleFor(d => d.KdDana).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, DPADanaR>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DPADanaR> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<DPADanaR>(request);

        if (!await _context.DPADanaR.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}