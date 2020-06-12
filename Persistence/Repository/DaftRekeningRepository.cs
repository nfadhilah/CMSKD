using Dapper;
using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Persistence.Repository
{
  public class DaftRekeningRepository : DapperRepository<DaftRekening>
  {
    public DaftRekeningRepository(IDbConnection connection) : base(connection)
    {
    }

    public DaftRekeningRepository(
      IDbConnection connection, ISqlGenerator<DaftRekening> sqlGenerator) : base(
      connection, sqlGenerator)
    {
    }

    public async Task<IEnumerable<dynamic>> GetDaftRekeningNodes(int? stAktif, string IdBank)
    {
      var builder = new SqlBuilder();

      var cmd = builder.AddTemplate(@"SELECT d.IDUNIT as IdUnit,
       d.STAKTIF as StAktif,
       d.IDBANK as IdBank,
       d.IDPEG as IdPeg,
       d.REKDaftRekening as RekDaftRekening,
       CASE
           WHEN EXISTS
                (
                    SELECT TOP (1)
                           1
                    FROM dbo.DaftRekening d2
                    WHERE d2.IDBANK = d.IDBANK
                          AND d2.STAKTIF = d.STAKTIF
                ) THEN
               0
           ELSE
               1
       END AS IsLeaf
FROM dbo.DaftRekening d
/**where**/
ORDER BY d.IDBANK");

      if (stAktif.HasValue)
        builder.Where("d.STAKTIF = @StAktif", new { StAktif = stAktif });

      if (!string.IsNullOrWhiteSpace(IdBank))
        builder.Where("d.IDBANK = @IdBank", new { IdBank = IdBank });

      return await Connection.QueryAsync(cmd.RawSql, cmd.Parameters);
    }
  }
}