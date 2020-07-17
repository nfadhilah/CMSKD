using Dapper;
using Domain.TUBEND;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Common;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Persistence.Repository.TUBEND
{
  public class SPPBPKRepository : CommonRepository<SPPBPK>
  {
    public SPPBPKRepository(IDbConnection connection) : base(connection) { }
    public SPPBPKRepository(IDbConnection connection, ISqlGenerator<SPPBPK> sqlGenerator) : base(connection, sqlGenerator) { }

    public async Task<IEnumerable<dynamic>> PopulateSPPDetRAsync(
      long idSPP, IDbTransaction transaction = null)
    {
      return await Connection.QueryAsync(@"SELECT b2.IDREK AS IdRek,
       b2.IDKEG AS IdKeg,
	   b2.IDNOJETRA AS IdNoJeTra,
       SUM(ISNULL(b2.NILAI, 0)) AS Nilai
FROM dbo.SPPBPK s
    LEFT JOIN dbo.BPK b
        ON b.IDBPK = s.IDBPK
    LEFT JOIN dbo.BPKDETR b2
        ON b.IDBPK = b2.IDBPK
WHERE s.IDSPP = @IdSPP
      AND b2.IDREK IS NOT NULL
      AND b2.IDKEG IS NOT NULL
      AND b2.IDNOJETRA IS NOT NULL
GROUP BY b2.IDREK,
         b2.IDKEG,
		 b2.IDNOJETRA;", new { IdSPP = idSPP }, transaction);
    }
  }
}
