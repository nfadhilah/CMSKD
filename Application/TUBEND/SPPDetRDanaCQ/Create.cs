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

namespace Application.TUBEND.SPPDetRDanaCQ
{
  public class Create
  {
    public class Command : IRequest<SPPDetRDanaDTO>
    {
      public long IdSPPDetR { get; set; }
      public long IdJDana { get; set; }
      public decimal? Nilai { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdSPPDetR).NotEmpty();
        RuleFor(d => d.IdJDana).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, SPPDetRDanaDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPPDetRDanaDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<SPPDetRDana>(request);

        if (!await _context.SPPDetRDana.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.SPPDetRDana
          .FindAllAsync<SPPDetR, JDana>(
            x => x.IdSPPDetRDana == added.IdSPPDetRDana, x => x.SPPDetR,
            x => x.JDana);

        return _mapper.Map<SPPDetRDanaDTO>(result.SingleOrDefault());
      }
    }
  }
}