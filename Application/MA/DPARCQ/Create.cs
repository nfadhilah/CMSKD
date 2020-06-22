using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.MA;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MA.DPARCQ
{
  public class Create
  {
    public class Command : IRequest<DPAR>
    {
      public long IdDPA { get; set; }
      public string KdTahap { get; set; }
      public int IdXKode { get; set; }
      public long IdKeg { get; set; }
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
        RuleFor(d => d.KdTahap).NotEmpty();
        RuleFor(d => d.IdXKode).NotEmpty();
        RuleFor(d => d.IdKeg).NotEmpty();
        RuleFor(d => d.IdRek).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, DPAR>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DPAR> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<DPAR>(request);

        if (!await _context.DPAR.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}