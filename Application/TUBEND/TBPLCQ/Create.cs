using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.TBPLCQ
{
  public class Create
  {
    public class Command : IRequest<TBPL>
    {
      public long IdUnit { get; set; }
      public string NoTBPL { get; set; }
      public long IdBend { get; set; }
      public string KdStatus { get; set; }
      public int IdxKode { get; set; }
      public DateTime? TglTBPL { get; set; }
      public string Penyetor { get; set; }
      public string Alamat { get; set; }
      public string UraiTBPL { get; set; }
      public DateTime? TglValid { get; set; }
      public long? KdRilis { get; set; }
      public int? StKirim { get; set; }
      public int? StCair { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdUnit).NotEmpty();
        RuleFor(d => d.NoTBPL).NotEmpty();
        RuleFor(d => d.KdStatus).NotEmpty();
        RuleFor(d => d.IdxKode).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, TBPL>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<TBPL> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<TBPL>(request);

        if (!await _context.TBPL.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}