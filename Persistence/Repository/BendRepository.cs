using Dapper;
using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Persistence.Repository
{
  public class BendRepository : DapperRepository<Bend>
  {
    public BendRepository(IDbConnection connection) : base(connection)
    {
    }

    public BendRepository(
      IDbConnection connection, ISqlGenerator<Bend> sqlGenerator) : base(
      connection, sqlGenerator)
    {
    }

    public async Task<IEnumerable<dynamic>> GetBendNodes(int? stAktif, string kdBank)
    {
      var builder = new SqlBuilder();

      var cmd = builder.AddTemplate(@"SELECT d.UNITKEY as UnitKey,
       d.STAKTIF as StAktif,
       d.KDBANK as KdBank,
       d.NIP as NIP,
       d.REKBEND as RekBend,
       CASE
           WHEN EXISTS
                (
                    SELECT TOP (1)
                           1
                    FROM dbo.BEND d2
                    WHERE d2.KDBANK LIKE d.KDBANK + '%'
                          AND d2.STAKTIF = d.STAKTIF
                ) THEN
               0
           ELSE
               1
       END AS IsLeaf
FROM dbo.BEND d
/**where**/
ORDER BY d.KDBANK");

      if (stAktif.HasValue)
        builder.Where("d.STAKTIF = @StAktif", new { StAktif = stAktif });

      if (!string.IsNullOrWhiteSpace(kdBank))
        builder.Where("d.KDBANK LIKE @KdBank + '%'", new { KdBank = kdBank });

      return await Connection.QueryAsync(cmd.RawSql, cmd.Parameters);
    }
  }
}