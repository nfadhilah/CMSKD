using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.MA;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MA.DPABCQ
{
  public class Create
  {
    public class Command : IRequest<DPAB>
    {
      public long IdDPA { get; set; }
      public int? IdXKode { get; set; }
      public string KdTahap { get; set; }
      public long IdRek { get; set; }
      public Decimal? Nilai { get; set; }
      public DateTime? DateCreate { get; set; }
      public DateTime? DateUpdate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdDPA).NotEmpty();
        RuleFor(d => d.IdRek).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, DPAB>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DPAB> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<DPAB>(request);

        if (!await _context.DPAB.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}