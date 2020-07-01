using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
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
    public class Command : IRequest<BPKDetRDanaDTO>
    {
      public long IdBPKDetR { get; set; }
      public long IdJDana { get; set; }
      public decimal? Nilai { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdBPKDetR).NotEmpty();
        RuleFor(d => d.IdJDana).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, BPKDetRDanaDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BPKDetRDanaDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<BPKDetRDana>(request);

        if (!await _context.BPKDetRDana.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.BPKDetRDana
          .FindAllAsync<BPKDetR, JDana>(
            x => x.IdBPKDetRDana == added.IdBPKDetRDana, x => x.BPKDetR,
            x => x.JDana);

        return _mapper.Map<BPKDetRDanaDTO>(result);
      }
    }
  }
}