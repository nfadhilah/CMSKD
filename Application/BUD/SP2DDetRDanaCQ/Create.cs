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

namespace Application.BUD.SP2DDetRDanaCQ
{
  public class Create
  {
    public class Command : IRequest<SP2DDetRDana>
    {
      public long IdSP2DDetRDana { get; set; }
      public long IdSP2DDetR { get; set; }
      public long KdDana { get; set; }
      public decimal? Nilai { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdSP2DDetR).NotEmpty();
        RuleFor(d => d.KdDana).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, SP2DDetRDana>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SP2DDetRDana> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<SP2DDetRDana>(request);

        if (!await _context.SP2DDetRDana.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.SP2DDetRDana
          .FindAllAsync<SP2DDetR, JDana>(
            x => x.IdSP2DDetR == added.IdSP2DDetR, x => x.SP2DDetR,
            x => x.JDana);

        return result.SingleOrDefault();
      }
    }
  }
}