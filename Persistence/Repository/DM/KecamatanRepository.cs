using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Common;

namespace Persistence.Repository.DM
{
  public class KecamatanRepository : CommonRepository<Kecamatan>
  {
    public KecamatanRepository(IDbConnection connection) : base(connection) { }

    public KecamatanRepository(
      IDbConnection connection, ISqlGenerator<Kecamatan> sqlGenerator) : base(
      connection, sqlGenerator) { }
  }
}