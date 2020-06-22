using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.MA;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MA.SPDDetBCQ
{
  public class Create
  {
    public class Command : IRequest<SPDDetB>
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

    public class Handler : IRequestHandler<Command, SPDDetB>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPDDetB> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<SPDDetB>(request);

        if (!await _context.SPDDetB.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}