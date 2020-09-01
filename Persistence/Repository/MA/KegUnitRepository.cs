using Dapper;
using Domain.MA;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Persistence.Repository.MA
{
  public class KegUnitRepository : DapperRepository<KegUnit>
  {
    public KegUnitRepository(IDbConnection connection) : base(connection) { }

    public KegUnitRepository(
      IDbConnection connection, ISqlGenerator<KegUnit> sqlGenerator) : base(
      connection, sqlGenerator) { }


    public async Task<IEnumerable<dynamic>> GetGroupKegUnitByKegInduk(
      long idUnit, long idPrgrm, string kdTahap)
    {
      var query = @"
SELECT m2.IDPRGRM AS IdPrgrm,
	   m2.IDKEG AS IdKeg,
       RTRIM(m2.NUKEG) AS NuKeg,
       m2.NMKEGUNIT AS NmKegUnit
FROM dbo.KEGUNIT k
    LEFT JOIN dbo.MKEGIATAN m
        ON m.IDKEG = k.IDKEG
    LEFT JOIN dbo.MKEGIATAN m2
        ON m2.IDKEG = m.IDKEGINDUK
WHERE k.IDUNIT = @IdUnit
      AND k.IDPRGRM = @IdPrgrm
      AND k.KDTAHAP = @KdTahap
GROUP BY m2.IDPRGRM,
		 m2.IDKEG,
         m2.NUKEG,
         m2.NMKEGUNIT;";

      return await Connection.QueryAsync(query,
        new {IdUnit = idUnit, IdPrgrm = idPrgrm, KdTahap = kdTahap});
    }


    public async Task<IEnumerable<dynamic>> GetTreeKegUnit(
      long idUnit, string kdTahap)
    {
      var builder = new SqlBuilder();

      var cmd = builder.AddTemplate(@"SELECT DISTINCT
       c.IdKeg,
       c.Kode,
       c.Label,
       c.Lvl,
       c.[Type]
FROM
(
    SELECT TRIM(d.KDURUS) + ' ' AS KdUrus,
           TRIM(d.NMURUS) AS NmUrus,
           TRIM(d.KDURUS) + TRIM(m2.NUPRGRM) AS NuPrgrm,
           TRIM(m2.NMPRGRM) AS NmPrgrm,
           TRIM(d.KDURUS) + TRIM(m2.NUPRGRM) + TRIM(m3.NUKEG) AS NuKeg,
           TRIM(m3.NMKEGUNIT) AS NmKegUnit,
           TRIM(d.KDURUS) + TRIM(m2.NUPRGRM) + TRIM(m3.NUKEG) + TRIM(m.NUKEG) AS NuSubKeg,
           TRIM(m.NMKEGUNIT) AS NmSubKegUnit,
           m3.IDKEG AS IdKeg,
           m.IDKEG AS IdSubKeg
    FROM dbo.KEGUNIT k
        LEFT JOIN dbo.DAFTUNIT d2
            ON d2.IDUNIT = k.IDUNIT
        LEFT JOIN dbo.MKEGIATAN m
            ON m.IDKEG = k.IDKEG
        LEFT JOIN dbo.MKEGIATAN m3
            ON m.IDKEGINDUK = m3.IDKEG
        LEFT JOIN dbo.MPGRM m2
            ON m2.IDPRGRM = m.IDPRGRM
        LEFT JOIN dbo.DAFTURUS d
            ON d.IDURUS = m2.IDURUS
    WHERE k.IDUNIT = @IdUnit
          AND k.KDTAHAP = @KdTahap
) AS a
    CROSS APPLY
(
    SELECT 1,
           a.KdUrus,
           a.NmUrus,
           NULL,
           'H'
    UNION ALL
    SELECT 2,
           a.NuPrgrm,
           a.NmPrgrm,
           NULL,
           'H'
    UNION ALL
    SELECT 3,
           a.NuKeg,
           a.NmKegUnit,
           a.IdKeg,
           'H'
    UNION ALL
    SELECT 4,
           a.NuSubKeg,
           a.NmSubKegUnit,
           a.IdSubKeg,
           'D'
) AS c(Lvl, Kode, Label, IdKeg, [Type])
ORDER BY c.Lvl,
         c.Kode;",
        new {IdUnit = idUnit, KdTahap = kdTahap});

      return await Connection.QueryAsync(cmd.RawSql, cmd.Parameters);
    }
  }
}