using Dapper;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repository.DM
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

    public async Task<IEnumerable<Bend>> GetBend(
      long? idPeg, string jnsBend, string rekBend, string idBank, long? idUnit)
    {
      var builder = new SqlBuilder();
      var cmd = builder.AddTemplate(@"SELECT b.*,
       p.*
FROM dbo.BEND b
    INNER JOIN dbo.PEGAWAI p
        ON p.IDPEG = b.IDPEG
/**where**/;");

      if (idUnit.HasValue)
        builder.Where("p.IDUNIT = @IdUnit", new { IdUnit = idUnit });

      if (idPeg.HasValue)
        builder.Where("p.IDPEG = @IdPeg", new { IdPeg = idPeg });

      if (!string.IsNullOrWhiteSpace(jnsBend))
        builder.Where("p.JNSBEND = @JnsBend", new { JsnBend = jnsBend });

      if (!string.IsNullOrWhiteSpace(rekBend))
        builder.Where("p.REKBEND = @RekBend", new { RekBend = rekBend });

      if (!string.IsNullOrWhiteSpace(idBank))
        builder.Where("p.IDBANK = @IdBank", new { IdBank = idBank });

      var result = (await Connection.QueryAsync<Bend, Pegawai, Bend>(cmd.RawSql,
        (bend, pegawai) =>
        {
          bend.Peg = pegawai;
          return bend;
        }, cmd.Parameters, splitOn: "IDPEG")).ToList();

      return result;
    }
  }
}