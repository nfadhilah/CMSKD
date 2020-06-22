using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.STSCQ
{
  public class Create
  {
    public class Command : IRequest<STS>
    {
      public long IdUnit { get; set; }
      public string NoSTS { get; set; }
      public long IdBend { get; set; }
      public string KdStatus { get; set; }
      public int IdxKode { get; set; }
      public long IdKas { get; set; }
      public DateTime? TglSTS { get; set; }
      public string Uraian { get; set; }
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
        RuleFor(d => d.NoSTS).NotEmpty();
        RuleFor(d => d.IdBend).NotEmpty();
        RuleFor(d => d.KdStatus).NotEmpty();
        RuleFor(d => d.IdxKode).NotEmpty();
        RuleFor(d => d.IdKas).NotEmpty();
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

      public async Task<STS> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<STS>(request);

        if (!await _context.STS.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}