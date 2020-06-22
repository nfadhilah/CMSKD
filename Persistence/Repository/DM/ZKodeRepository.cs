using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.DM
{
  public class ZKodeRepository : DapperRepository<ZKode>
  {
    public ZKodeRepository(IDbConnection connection) : base(connection) { }
    public ZKodeRepository(IDbConnection connection, ISqlGenerator<ZKode> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
