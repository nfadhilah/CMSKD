using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
{
  public class PARepository : DapperRepository<PA>
  {
    public PARepository(IDbConnection connection) : base(connection) { }
    public PARepository(IDbConnection connection, ISqlGenerator<PA> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
