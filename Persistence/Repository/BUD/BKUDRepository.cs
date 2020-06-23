using Domain.BUD;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.BUD
{
  public class BKUDRepository : DapperRepository<BKUD>
  {
    public BKUDRepository(IDbConnection connection) : base(connection) { }
    public BKUDRepository(IDbConnection connection, ISqlGenerator<BKUD> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
