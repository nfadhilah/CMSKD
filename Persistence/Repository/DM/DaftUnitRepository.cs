using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.DM
{
  public class DaftUnitRepository : DapperRepository<DaftUnit>
  {
    public DaftUnitRepository(IDbConnection connection) : base(connection)
    {
    }

    public DaftUnitRepository(IDbConnection connection, ISqlGenerator<DaftUnit> sqlGenerator) : base(connection, sqlGenerator)
    {
    }

    public async Task<IEnumerable<dynamic>> GetDaftUnitNodes(int? kdLevel, string kdUnit)
    {
      var builder = new SqlBuilder();

      var cmd = builder.AddTemplate(@"SELECT d.IDUNIT as IdUnit,
       d.KDLEVEL as KdLevel,
       d.KDUNIT as KdUnit,
       d.NMUNIT as NmUnit,
       d.TYPE as Type,
       CASE
           WHEN EXISTS
                (
                    SELECT TOP (1)
                           1
                    FROM dbo.DAFTUNIT d2
                    WHERE d2.KDUNIT LIKE d.KDUNIT + '%'
                          AND d2.KDLEVEL = d.KDLEVEL + 1
                ) THEN
               0
           ELSE
               1
       END AS IsLeaf
FROM dbo.DAFTUNIT d
/**where**/
ORDER BY d.KDUNIT");

      if (kdLevel.HasValue)
        builder.Where("d.KDLEVEL = @KdLevel", new { KdLevel = kdLevel });

      if (!string.IsNullOrWhiteSpace(kdUnit))
        builder.Where("d.KDUNIT LIKE @KdUnit + '%'", new { KdUnit = kdUnit });

      return await Connection.QueryAsync(cmd.RawSql, cmd.Parameters);
    }
  }
}
