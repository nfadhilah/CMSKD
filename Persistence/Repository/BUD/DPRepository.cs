using Domain.BUD;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.BUD
{
  public class DPRepository : DapperRepository<DP>
  {
    public DPRepository(IDbConnection connection) : base(connection) { }
    public DPRepository(IDbConnection connection, ISqlGenerator<DP> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
