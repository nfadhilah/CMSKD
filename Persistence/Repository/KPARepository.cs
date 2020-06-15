using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
{
  public class KPARepository : DapperRepository<KPA>
  {
    public KPARepository(IDbConnection connection) : base(connection) { }
    public KPARepository(IDbConnection connection, ISqlGenerator<KPA> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
