using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.BUD;
using Domain.DM;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BUD.BKUDCQ
{
  public class Create
  {
    public class Command : IRequest<BKUDDTO>
    {
      public long? IdKas { get; set; }
      public long IdSTS { get; set; }
      public long? IdBKas { get; set; }
      public long? IdUnit { get; set; }
      public DateTime? TglKas { get; set; }
      public DateTime? TglValid { get; set; }
      public string Uraian { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdUnit).NotEmpty();
        RuleFor(d => d.IdSTS).NotEmpty();
        RuleFor(d => d.IdKas).NotEmpty();
        RuleFor(d => d.IdBKas).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, BKUDDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BKUDDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<BKUD>(request);

        if (!await _context.BKUD.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.BKUD
          .FindAllAsync<DaftUnit, STS>(
            x => x.IdBKUD == added.IdBKUD, x => x.Unit, x => x.STS);

        return _mapper.Map<BKUDDTO>(result.SingleOrDefault());
      }
    }
  }
}