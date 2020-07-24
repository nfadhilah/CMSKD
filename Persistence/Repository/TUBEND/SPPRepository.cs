using Dapper;
using Domain.DM;
using Domain.MA;
using Domain.TUBEND;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Persistence.Repository.TUBEND
{
  public interface ISPPSqlParam
  {
    public long? IdUnit { get; set; }
    public string NoSPP { get; set; }
    public string KdStatus { get; set; }
    public int? KdBulan { get; set; }
    public long? IdBend { get; set; }
    public long? IdSPD { get; set; }
    public long? IdPhk3 { get; set; }
    public int? IdxKode { get; set; }
    public string NoReg { get; set; }
    public string KetOtor { get; set; }
    public long? IdKontrak { get; set; }
    public string NoKontrak { get; set; }
    public string Keperluan { get; set; }
    public string Penolakan { get; set; }
    public DateTime? TglValid { get; set; }
    public DateTime? TglSPP { get; set; }
    public string Status { get; set; }
    public DateTime? DateCreate { get; set; }
    public long? IdKeg { get; set; }
    public bool? IsValid { get; set; }
  }

  public class SPPRepository : CommonRepository<SPP>
  {
    public SPPRepository(IDbConnection connection) : base(connection) { }

    public SPPRepository(
      IDbConnection connection, ISqlGenerator<SPP> sqlGenerator) : base(
      connection, sqlGenerator)
    { }

    public IEnumerable<SPP> GetSPP(
      ISPPSqlParam queryParams, uint limit, uint offset)
    {
      var builder = new SqlBuilder();

      var cmd = builder.AddTemplate(@"SELECT DISTINCT s.*,
       d.*,
       b.*,
       s3.*,
       d2.*,
       k.*
FROM dbo.SPP s
    INNER JOIN dbo.DAFTUNIT d
        ON d.IDUNIT = s.IDUNIT
    INNER JOIN dbo.BEND b
        ON b.IDBEND = s.IDBEND
    INNER JOIN dbo.SPD s3
        ON s3.IDSPD = s.IDSPD
    LEFT JOIN dbo.KONTRAK as k
        ON k.IDKONTRAK = s.IDKONTRAK
    LEFT JOIN dbo.DAFTPHK3 d2
        ON d2.IDPHK3 = s.IDPHK3 /**leftjoin**/ /**where**/");

      if (queryParams.IdUnit.HasValue)
        builder.Where("s.IDUNIT = @IdUnit", new { queryParams.IdUnit });

      if (queryParams.IdKontrak.HasValue)
        builder.Where("s.IDKONTRAK = @IdKontrak", new { queryParams.IdKontrak });

      if (!string.IsNullOrWhiteSpace(queryParams.NoKontrak))
        builder.Where("k.NOKONTRAK = @NoKontrak", new { queryParams.NoKontrak });

      if (!string.IsNullOrWhiteSpace(queryParams.NoSPP))
        builder.Where("s.NOSPP LIKE @NoSPP",
          new { NoSPP = "%" + queryParams.NoSPP } + "%");

      if (!string.IsNullOrWhiteSpace(queryParams.KdStatus))
        builder.Where("s.KDSTATUS = @KdStatus", new { queryParams.KdStatus });

      if (queryParams.IdPhk3.HasValue)
        builder.Where("s.IDPHK3 = @IdPhk3", new { queryParams.IdPhk3 });

      if (queryParams.KdBulan.HasValue)
        builder.Where("s.KDBULAN = @KdBulan", new { queryParams.KdBulan });

      if (queryParams.IdBend.HasValue)
        builder.Where("s.IDBEND = @IdBend", new { queryParams.IdBend });

      if (queryParams.IdSPD.HasValue)
        builder.Where("s.IDSPD = @IdSPD", new { queryParams.IdSPD });

      if (queryParams.IdxKode.HasValue)
        builder.Where("s.IDXKODE = @IdxKode", new { queryParams.IdxKode });

      if (queryParams.TglSPP.HasValue)
        builder.Where("s.TGLSPP = @TglSPP", new { queryParams.TglSPP });

      if (queryParams.TglValid.HasValue)
        builder.Where("s.TGLVALID = @TglValid", new { queryParams.TglValid });

      if (!string.IsNullOrWhiteSpace(queryParams.NoReg))
        builder.Where("s.NOREG LIKE @NoReg",
          new { NoReg = "%" + queryParams.NoReg + "%" });

      if (!string.IsNullOrWhiteSpace(queryParams.KetOtor))
        builder.Where("s.KETOTOR LIKE @KetOtor",
          new { KetOtor = "%" + queryParams.KetOtor + "%" });

      if (!string.IsNullOrWhiteSpace(queryParams.Keperluan))
        builder.Where("s.KEPERLUAN LIKE @Keperluan",
          new { Keperluan = "%" + queryParams.Keperluan + "%" });

      if (!string.IsNullOrWhiteSpace(queryParams.Penolakan))
        builder.Where("s.PENOLAKAN LIKE @Penolakan",
          new { Penolakan = "%" + queryParams.Penolakan + "%" });

      if (queryParams.IsValid.HasValue)
        builder.Where("s.TGLVALID IS NOT NULL");

      if (queryParams.IdKeg.HasValue)
      {
        builder.LeftJoin("SPPDETR as sr ON s.IDSPP = sr.IDSPP").Where(
          @"(
          ISNULL(sr.IDKEG, '') = ISNULL(@IdKeg, '')
          OR ISNULL(@IdKeg, '') = ''
          OR
          (
              (sr.IDKEG IS NULL)
              AND @IdKeg IS NOT NULL
          ))",
          new { queryParams.IdKeg });
      }

      var paginatedQuery = PaginatedQueryBuilder(cmd, offset, limit,
        new List<string> { "s.NOSPP ASC" });

      return Connection
        .Query<SPP, DaftUnit, Bend, SPD, DaftPhk3, Kontrak, SPP>(
          paginatedQuery.RawSql,
          (spp, unit, bend, spd, phk3, kontrak) =>
          {
            spp.Unit = unit;
            spp.Bendahara = bend;
            spp.SPD = spd;
            if (phk3 != null) spp.Phk3 = phk3;
            if (kontrak != null) spp.Kontrak = kontrak;
            return spp;
          }, cmd.Parameters,
          splitOn: "IDUNIT, IDBEND, IDSPD, IDPHK3, IDKONTRAK").Distinct()
        .ToList();
    }
  }
}