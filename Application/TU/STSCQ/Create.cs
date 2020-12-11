using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TU;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TU.STSCQ
{
  public class Create
  {
    public class Command : IRequest<STS>
    {
      public string UnitKey { get; set; }
      public string NoSTS { get; set; }
      public string KeyBend1 { get; set; }
      public string KdStatus { get; set; }
      public string IdXKode { get; set; }
      public string KeyBend2 { get; set; }
      public string IdxTtd { get; set; }
      public string NoBBantu { get; set; }
      public DateTime? TglSts { get; set; }
      public string Uraian { get; set; }
      public DateTime? TglValid { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(x => x.UnitKey).NotEmpty();
        RuleFor(x => x.NoSTS).NotEmpty();
        RuleFor(x => x.KdStatus).NotEmpty();
        RuleFor(x => x.IdXKode).NotEmpty();
        RuleFor(x => x.NoBBantu).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, STS>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<STS> Handle(Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<STS>(request);

        if (!await _context.STS.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.STS.FindAsync(x => x.UnitKey == added.UnitKey && x.NoSTS == added.NoSTS);

        return result;
      }
    }
  }
}