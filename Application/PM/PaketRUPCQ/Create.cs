using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
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
      public JnsRUP JnsRUP { get; set; }
      public int TipeSwakelola { get; set; }
      public string UraiTipeSwakelola { get; set; }
      public long IdKeg { get; set; }
      public decimal? NilaiPagu { get; set; }
      public DateTime? TglValid { get; set; }
      public string KodeRUP { get; set; }
      public string NmPaket { get; set; }
      public string IdLokasi { get; set; }
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
      public bool? A { get; set; }
      public bool? FD { get; set; }
      public bool? U { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.JnsRUP).NotEmpty();
        // RuleFor(d => d.TipeSwakelola).NotEmpty();
        // RuleFor(d => d.UraiTipeSwakelola).NotEmpty();
        RuleFor(d => d.IdLokasi).NotEmpty();
        RuleFor(d => d.Lokasi).NotEmpty();
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
      private readonly IUserAccessor _userAccessor;

      public Handler(
        IDbContext context, IMapper mapper, IUserAccessor userAccessor)
      {
        _context = context;
        _mapper = mapper;
        _userAccessor = userAccessor;
      }

      public async Task<PaketRUPDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var userAct = await _context.UserKegiatan.GetListUserKegiatan(
          _userAccessor.GetCurrentUsername(), new List<long> {request.IdKeg});

        if (!userAct.Any())
          throw new ApiException(
            "User anda tidak memiliki otorisasi untuk kegiatan ini",
            (int) HttpStatusCode.Unauthorized);

        var added = _mapper.Map<PaketRUP>(request);

        added.CreatedBy = _userAccessor.GetCurrentUsername();

        if (!await _context.PaketRup.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.PaketRup
          .FindAllAsync<DaftUnit, MKegiatan, JPekerjaan, JDana, DaftPhk3,
            MetodePengadaan>(
            x => x.IdRUP == added.IdRUP,
            c => c.Unit,
            c => c.Keg, c => c.JnsPekerjaan, c => c.JDana, c => c.Phk3,
            c => c.MetodePengadaan);

        return _mapper.Map<PaketRUPDTO>(result.SingleOrDefault());
      }
    }
  }
}