using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.BPKDetRPCQ
{
  public class Create
  {
    public class Command : IRequest<BPKDetRP>
    {
      public long IdBPKDetR { get; set; }
      public long IdPajak { get; set; }
      public decimal? Nilai { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdBPKDetR).NotEmpty();
        RuleFor(d => d.IdPajak).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, BPKDetRP>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BPKDetRP> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<BPKDetRP>(request);

        if (!await _context.BPKDetRP.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}