using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.DM
{
  public class KPARepository : DapperRepository<KPA>
  {
    public KPARepository(IDbConnection connection) : base(connection) { }
    public KPARepository(IDbConnection connection, ISqlGenerator<KPA> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
