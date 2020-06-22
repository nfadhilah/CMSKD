using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.BPKDetRDanaCQ
{
  public class Create
  {
    public class Command : IRequest<BPKDetRDana>
    {
      public long IdBPKDetR { get; set; }
      public string KdDana { get; set; }
      public decimal? Nilai { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdBPKDetR).NotEmpty();
        RuleFor(d => d.KdDana).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, BPKDetRDana>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BPKDetRDana> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<BPKDetRDana>(request);

        if (!await _context.BPKDetRDana.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}