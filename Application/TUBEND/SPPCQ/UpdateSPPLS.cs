using Application.Interfaces;
using Application.TUBEND.SPPBACQ;
using Application.TUBEND.SPPDetRCQ;
using AutoMapper;
using AutoWrapper.Wrappers;
using Dapper;
using Domain.DM;
using Domain.MA;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.SPPCQ
{
  public class UpdateSPPLS
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdUnit { get; set; }

      public string NoSPP { get; set; }
      // public string KdStatus { get; set; }
      public int KdBulan { get; set; }
      public long? IdBend { get; set; }
      public long IdSPD { get; set; }
      public long? IdPhk3 { get; set; }
      // public int IdxKode { get; set; }
      public string NoReg { get; set; }
      public string KetOtor { get; set; }
      public long? IdKontrak { get; set; }
      public string Keperluan { get; set; }
      public string Penolakan { get; set; }
      public DateTime? TglValid { get; set; }
      public DateTime? TglSPP { get; set; }
      public string Status { get; set; }
      public DateTime? DateCreate { get; set; }
      public IEnumerable<long> IdBeritaList { get; set; }

      public DTO()
      {
        var config = new MapperConfiguration(opt =>
        {
          opt.CreateMap<DTO, Command>();
        });

        _mapper = config.CreateMapper();
      }

      public Command MapDTO(Command destination)
      {
        return _mapper.Map(this, destination);
      }
    }

    public class Validator : AbstractValidator<DTO>
    {
      public Validator()
      {
        RuleFor(d => d.IdUnit).NotEmpty();
        RuleFor(d => d.NoSPP).NotEmpty();
        // RuleFor(d => d.KdStatus).NotEmpty();
        RuleFor(d => d.KdBulan).NotEmpty();
        RuleFor(d => d.IdSPD).NotEmpty();
        // RuleFor(d => d.IdxKode).NotEmpty();
        RuleFor(d => d.IdBeritaList).NotEmpty();
      }
    }

    public class Command : SPP, IRequest<SPPLSResultDTO>
    {
      public IEnumerable<long> IdBeritaList { get; set; }
    }

    public class Handler : IRequestHandler<Command, SPPLSResultDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SPPLSResultDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        using var transaction = _context.Connection.BeginTransaction();

        try
        {
          var spp =
            await _context.SPP.FindByIdAsync(request.IdSPP, transaction);

          if (spp == null)
            throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

          if (spp.TglValid.HasValue)
            throw new ApiException(
              "Data tidak bisa diubah karena sudah valid");

          _mapper.Map(request, spp);

          await _context.SPP.UpdateAsync(spp, transaction);

          // Delete Insert SPPBA
          await _context.SPPBA.DeleteAsync(x => x.IdSPP == spp.IdSPP,
            transaction);

          var daftSPPBA = request.IdBeritaList.Select(id => new SPPBA
          {
            IdBerita = id,
            IdSPP = spp.IdSPP,
            DateCreate = DateTime.Now
          }).ToList();

          await _context.SPPBA.BulkInsertAsync(daftSPPBA, transaction);

          // Populate & Delete Insert SPPDETR from BERITADETR
          await _context.SPPDetR.DeleteAsync(x => x.IdSPP == spp.IdSPP,
            transaction);

          var daftBeritaDet =
            _context.Connection.Query<BeritaDetR, Berita, BeritaDetR>(@"SELECT *
FROM dbo.BERITADETR b
    INNER JOIN dbo.BERITA b2
        ON b2.IDBERITA = b.IDBERITA
WHERE b.IDBERITA IN @Ids;", (beritaDetR, berita) =>
              {
                beritaDetR.Berita = berita;
                return beritaDetR;
              }, new { Ids = request.IdBeritaList }, transaction,
              splitOn: "IDBERITA").ToList();

          var daftSPPDetR = new List<SPPDetR>();

          if (daftBeritaDet.Any())
          {
            daftSPPDetR.AddRange(daftBeritaDet
              .GroupBy(x => new { x.IdRek, x.Berita.IdKeg }, r => r).Select(b =>
                new SPPDetR
                {
                  DateCreate = DateTime.Now,
                  IdKeg = b.Key.IdKeg,
                  IdRek = b.Key.IdRek,
                  Nilai = b.Sum(x => x.Nilai),
                  IdNoJeTra = 21,
                  IdSPP = spp.IdSPP
                }));

            await _context.SPPDetR.BulkInsertAsync(daftSPPDetR, transaction);
          }

          transaction.Commit();

          var sppResult = await _context.SPP
            .FindAllAsync<DaftUnit, StatTrs, Bend, SPD, DaftPhk3, Kontrak>(
              x => x.IdSPP == spp.IdSPP, x => x.Unit,
              x => x.StatTrs, x => x.Bendahara, x => x.SPD, x => x.Phk3,
              x => x.Kontrak);

          var daftSPPBAResult = await
            _context.SPPBA.FindAllAsync<SPP, Berita>(x => x.IdSPP == spp.IdSPP, x => x.SPP,
              x => x.Berita);

          var daftSPPDetRResult = await _context.SPPDetR
            .FindAllAsync<DaftRekening, MKegiatan, SPP, JTrnlKas>(
              x => x.IdSPP == spp.IdSPP, x => x.Rekening,
              x => x.Kegiatan, x => x.SPP, x => x.JTrnlKas);

          var dto = new SPPLSResultDTO
          {
            SPP = _mapper.Map<SPPDTO>(sppResult.SingleOrDefault()),
            SPPBAList = _mapper.Map<IEnumerable<SPPBADTO>>(daftSPPBAResult),
            SPPDetRList = _mapper.Map<IEnumerable<SPPDetRDTO>>(daftSPPDetRResult)
          };

          return dto;
        }
        catch (Exception e)
        {
          transaction.Rollback();
          throw new ApiException(e.Message);
        }
      }
    }
  }
}