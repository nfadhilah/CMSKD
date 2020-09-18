using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Common;

namespace Persistence.Repository.DM
{
  public class KelurahanRepository : CommonRepository<Kelurahan>
  {
    public KelurahanRepository(IDbConnection connection) : base(connection) { }

    public KelurahanRepository(
      IDbConnection connection, ISqlGenerator<Kelurahan> sqlGenerator) : base(
      connection, sqlGenerator) { }
  }
}