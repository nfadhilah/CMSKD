using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.TBPLDetCQ
{
  public class Create
  {
    public class Command : IRequest<TBPLDet>
    {
      public long IdTBPL { get; set; }
      public long IdBend { get; set; }
      public int IdNoJetTra { get; set; }
      public decimal? Nilai { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdTBPL).NotEmpty();
        RuleFor(d => d.IdBend).NotEmpty();
        RuleFor(d => d.IdNoJetTra).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, TBPLDet>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<TBPLDet> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<TBPLDet>(request);

        if (!await _context.TBPLDet.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}