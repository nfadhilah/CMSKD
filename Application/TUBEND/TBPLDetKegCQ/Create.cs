using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.TBPLDetKegCQ
{
  public class Create
  {
    public class Command : IRequest<TBPLDetKeg>
    {
      public long IdTBPLDet { get; set; }
      public int IdNoJeTra { get; set; }
      public long IdKeg { get; set; }
      public decimal? Nilai { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdTBPLDet).NotEmpty();
        RuleFor(d => d.IdNoJeTra).NotEmpty();
        RuleFor(d => d.IdKeg).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, TBPLDetKeg>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<TBPLDetKeg> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<TBPLDetKeg>(request);

        if (!await _context.TBPLDetKeg.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}