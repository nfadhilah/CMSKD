using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TU;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TU.BKUDCQ
{
  public class Create
  {
    public class Command : IRequest<BKUD>
    {
      public string NoBuKas { get; set; }

      public string NoBBantu { get; set; }
      public string UnitKey { get; set; }
      public string NoSTS { get; set; }
      public string IdxTtd { get; set; }
      public string KdBukti { get; set; }
      public DateTime? TglKas { get; set; }
      public DateTime? TglValid { get; set; }
      public string Uraian { get; set; }
      public string NoBuktiKas { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(x => x.NoBuKas).NotEmpty();
        RuleFor(x => x.NoBBantu).NotEmpty();
        RuleFor(x => x.UnitKey).NotEmpty();
        RuleFor(x => x.KdBukti).NotEmpty();
      }
    }


    public class Handler : IRequestHandler<Command, BKUD>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BKUD> Handle(Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<BKUD>(request);

        if (!await _context.BKUD.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.BKUD.FindAsync(x => x.NoBuKas == request.NoBuKas);

        return result;
      }
    }
  }
}