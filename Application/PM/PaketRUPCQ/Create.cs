using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.TUBEND.SPMCQ;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.MA;
using Domain.PM;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.PM.PaketRUPCQ
{
  public class Create
  {
    public class Command : IRequest<PaketRUPDTO>
    {
      public long IdUnit { get; set; }
      public long IdKeg { get; set; }
      public decimal? NilaiPagu { get; set; }
      public DateTime? TglValid { get; set; }
      public string KodeRUP { get; set; }
      public string NmPaket { get; set; }
      public string Lokasi { get; set; }
      public string Volume { get; set; }
      public string UraiPaket { get; set; }
      public StatusRUP Status { get; set; }
      public long? IdJnsPekerjaan { get; set; }
      public long? IdMetodePengadaan { get; set; }
      public DateTime? AwalPekerjaan { get; set; }
      public DateTime? AkhirPekerjaan { get; set; }
      public long IdJDana { get; set; }
      public long? IdPhk3 { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdUnit).NotEmpty();
        RuleFor(d => d.IdKeg).NotEmpty();
        RuleFor(d => d.IdJDana).NotEmpty();
        RuleFor(d => d.KodeRUP).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, PaketRUPDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<PaketRUPDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<PaketRUP>(request);

        if (!await _context.PaketRup.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.PaketRup
          .FindAllAsync<DaftUnit, MKegiatan, JPekerjaan, JDana, DaftPhk3,
            MetodePengadaan>(
            x => x.IdRUP == added.IdRUP,
            c => c.Unit,
            c => c.Keg, c => c.JnsPekerjaan, c => c.JDana, c => c.Phk3,
            c => c.MetodePengadaan);

        // return _mapper.Map<PaketRUPDTO>(result.SingleOrDefault());
        return _mapper.Map<PaketRUPDTO>(result.SingleOrDefault());
      }
    }
  }
}