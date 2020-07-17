using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.MA;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.SPMCQ
{
  public class Create
  {
    public class Command : IRequest<SPMDTO>
    {
      public long IdUnit { get; set; }
      public string NoSPM { get; set; }
      public string KdStatus { get; set; }
      public long IdBend { get; set; }
      public long IdSPD { get; set; }
      public long IdSPP { get; set; }
      public long? IdPhk3 { get; set; }
      public int IdxKode { get; set; }
      public string NoReg { get; set; }
      public string KetOtor { get; set; }
      public long? IdKontrak { get; set; }
      public string Keperluan { get; set; }
      public string Penolakan { get; set; }
      public DateTime? TglValid { get; set; }
      public DateTime? TglSPM { get; set; }
      public DateTime? TglSPP { get; set; }
      public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdUnit).NotEmpty();
        RuleFor(d => d.NoSPM).NotEmpty();
        RuleFor(d => d.KdStatus).NotEmpty();
        RuleFor(d => d.IdBend).NotEmpty();
        RuleFor(d => d.IdSPD).NotEmpty();
        RuleFor(d => d.IdSPP).NotEmpty();
        RuleFor(d => d.IdxKode).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, SPMDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPMDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<SPM>(request);

        if (!await _context.SPM.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.SPM
          .FindAllAsync<DaftUnit, Bend, SPD, SPP, DaftPhk3>(
            x => x.IdSPM == added.IdSPM, x => x.Unit, x => x.Bend, x => x.SPD,
            x => x.SPP, x => x.Phk3);

        return _mapper.Map<SPMDTO>(result.SingleOrDefault());
      }
    }
  }
}