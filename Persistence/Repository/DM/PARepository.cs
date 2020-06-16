using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.DM
{
  public class PARepository : DapperRepository<PA>
  {
    public PARepository(IDbConnection connection) : base(connection) { }
    public PARepository(IDbConnection connection, ISqlGenerator<PA> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
