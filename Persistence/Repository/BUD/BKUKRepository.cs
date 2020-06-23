using Domain.BUD;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.BUD
{
  public class BKUKRepository : DapperRepository<BKUK>
  {
    public BKUKRepository(IDbConnection connection) : base(connection) { }
    public BKUKRepository(IDbConnection connection, ISqlGenerator<BKUK> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
