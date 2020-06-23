using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.BUD;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BUD.SP2DDetRPCQ
{
  public class Create
  {
    public class Command : IRequest<SP2DDetRP>
    {
      public long IdSP2DDetR { get; set; }
      public long IdPajak { get; set; }
      public decimal? Nilai { get; set; }
      public string Keterangan { get; set; }
      public string IdBilling { get; set; }
      public DateTime? TglBilling { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdSP2DDetR).NotEmpty();
        RuleFor(d => d.IdPajak).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, SP2DDetRP>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SP2DDetRP> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<SP2DDetRP>(request);

        if (!await _context.SP2DDetRP.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}