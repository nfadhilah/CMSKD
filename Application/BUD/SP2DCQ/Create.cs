using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.BUD;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BUD.SP2DCQ
{
  public class Create
  {
    public class Command : IRequest<SP2D>
    {
      public string NoSP2D { get; set; }
      public long IdUnit { get; set; }
      public string KdStatus { get; set; }
      public long IdSPM { get; set; }
      public string NoSPM { get; set; }
      public long? IdBend { get; set; }
      public long? IdSPD { get; set; }
      public long? IdPhk3 { get; set; }
      public long? IdTtd { get; set; }
      public int? IdxKode { get; set; }
      public string NoReg { get; set; }
      public string KetOtor { get; set; }
      public string NoKontrak { get; set; }
      public string Keperluan { get; set; }
      public string Penolakan { get; set; }
      public DateTime? TglValid { get; set; }
      public DateTime? TglSP2D { get; set; }
      public DateTime? TglSPM { get; set; }
      public string NoBBantu { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdUnit).NotEmpty();
        RuleFor(d => d.NoSP2D).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, SP2D>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SP2D> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<SP2D>(request);

        if (!await _context.SP2D.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}