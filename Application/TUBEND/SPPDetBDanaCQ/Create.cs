using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.SPPDetBDanaCQ
{
  public class Create
  {
    public class Command : IRequest<SPPDetBDanaDTO>
    {
      public long IdSPPDetB { get; set; }
      public long IdJDana { get; set; }
      public decimal? Nilai { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdSPPDetB).NotEmpty();
        RuleFor(d => d.IdJDana).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, SPPDetBDanaDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPPDetBDanaDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<SPPDetBDana>(request);

        if (!await _context.SPPDetBDana.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.SPPDetBDana
          .FindAllAsync<SPPDetR, JDana>(
            x => x.IdSPPDetBDana == added.IdSPPDetBDana, x => x.SPPDetB,
            x => x.JDana);

        return _mapper.Map<SPPDetBDanaDTO>(result.SingleOrDefault());
      }
    }
  }
}