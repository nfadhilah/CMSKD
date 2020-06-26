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
      connection, sqlGenerator)
    {
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
           TRIM(d.KDURUS) + TRIM(m2.NUPRGRM) + TRIM(m.NUKEG) AS NuKeg,
           TRIM(m.NMKEGUNIT) AS NmKegUnit,
           m.IDKEG AS IdKeg
    FROM dbo.KEGUNIT k
        LEFT JOIN dbo.DAFTUNIT d2
            ON d2.IDUNIT = k.IDUNIT
        LEFT JOIN dbo.MKEGIATAN m
            ON m.IDKEG = k.IDKEG
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
           'D'
) AS c(Lvl, Kode, Label, IdKeg, [Type]);",
        new { IdUnit = idUnit, KdTahap = kdTahap });

      return await Connection.QueryAsync(cmd.RawSql, cmd.Parameters);
    }
  }
}