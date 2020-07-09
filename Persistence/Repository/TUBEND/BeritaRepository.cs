using Dapper;
using Domain.DM;
using Domain.TUBEND;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Persistence.Repository.TUBEND
{
  public interface IBeritaSqlParam
  {
    public long? IdUnit { get; set; }
    public long? IdKeg { get; set; }
    public string NoBerita { get; set; }
    public DateTime? TglBA { get; set; }
    public long? IdKontrak { get; set; }
    public string Urai_Berita { get; set; }
    public DateTime? TglValid { get; set; }
    public string KdStatus { get; set; }
    public DateTime? DateCreate { get; set; }
    public bool IsValid { get; set; }
  }

  public class BeritaRepository : CommonRepository<Berita>
  {
    public BeritaRepository(IDbConnection connection) : base(connection) { }
    public BeritaRepository(IDbConnection connection, ISqlGenerator<Berita> sqlGenerator) : base(connection, sqlGenerator) { }

    public Task<IEnumerable<Berita>> GetBerita(
      IBeritaSqlParam sqlParam, uint offset, uint limit)
    {
      var builder = new SqlBuilder();

      var cmd = builder.AddTemplate(@"SELECT *
FROM dbo.BERITA b
    INNER JOIN dbo.DAFTUNIT d2
        ON d2.IDUNIT = b.IDUNIT
    INNER JOIN dbo.MKEGIATAN m
        ON m.IDKEG = b.IDKEG
    INNER JOIN dbo.KONTRAK k
        ON k.IDKONTRAK = b.IDKONTRAK
    INNER JOIN dbo.DAFTPHK3 d
        ON d.IDPHK3 = k.IDPHK3
/**where**/
");

      if (sqlParam.IsValid)
        builder.Where("b.TGLVALID IS NOT NULL");

      if (sqlParam.IdUnit.HasValue)
        builder.Where("b.IDUNIT = @IdUnit", new { sqlParam.IdUnit });

      if (!string.IsNullOrWhiteSpace(sqlParam.KdStatus))
        builder.Where("b.KDSTATUS = @KdStatus", new { sqlParam.KdStatus });

      if (!string.IsNullOrWhiteSpace(sqlParam.NoBerita))
        builder.Where("b.NOBERITA = @NoBerita", new { sqlParam.NoBerita });

      if (!string.IsNullOrWhiteSpace(sqlParam.Urai_Berita))
        builder.Where("b.URAI_BERITA LIKE @Urai_Berita",
          new { Urai_Berita = "%" + sqlParam.Urai_Berita + "%" });

      if (sqlParam.IdKontrak.HasValue)
        builder.Where("b.IDKONTRAK = @IdKontrak", new { sqlParam.IdKontrak });

      if (sqlParam.IdKeg.HasValue)
        builder.Where("b.IDKEG = @IdKeg", new { sqlParam.IdKeg });

      if (sqlParam.TglBA.HasValue)
        builder.Where("b.TGLBA = @TglBA", new { sqlParam.TglBA });

      if (sqlParam.DateCreate.HasValue)
        builder.Where("b.DATECREATE = @DateCreate", new { sqlParam.DateCreate });

      if (sqlParam.TglValid.HasValue)
        builder.Where("b.TGVALID = @TglValid", new { sqlParam.TglValid });

      var paginatedQuery = PaginatedQueryBuilder(cmd, offset, limit, new List<string> { "NoBA ASC" });

      var result =
        Connection
          .QueryAsync<Berita, DaftUnit, MKegiatan, Kontrak, DaftPhk3, Berita>(
            cmd.RawSql, (
              (berita, unit, mKegiatan, kontrak, daftPhk3) =>
              {
                berita.Unit = unit;
                berita.Kegiatan = mKegiatan;
                berita.Kontrak = kontrak;
                berita.Phk3 = daftPhk3;
                return berita;
              }), cmd.Parameters, splitOn: "IDUNIT, IDKEG, IDKONTRAK, IDPHK3");

      return result;
    }
  }
}
