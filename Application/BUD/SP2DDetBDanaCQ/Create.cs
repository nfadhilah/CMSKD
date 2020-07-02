using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.BUD;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BUD.SP2DDetBDanaCQ
{
  public class Create
  {
    public class Command : IRequest<SP2DDetBDanaDTO>
    {
      public long IdSP2DDetB { get; set; }
      public long IdJDana { get; set; }
      public decimal? Nilai { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdSP2DDetB).NotEmpty();
        RuleFor(d => d.IdJDana).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, SP2DDetBDanaDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SP2DDetBDanaDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<SP2DDetBDana>(request);

        if (!await _context.SP2DDetBDana.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.SP2DDetBDana
          .FindAllAsync<SP2DDetB, JDana>(
            x => x.IdSP2DDetBDana == added.IdSP2DDetBDana, x => x.SP2DDetB,
            x => x.JDana);

        return _mapper.Map<SP2DDetBDanaDTO>(result.SingleOrDefault());
      }
    }
  }
}