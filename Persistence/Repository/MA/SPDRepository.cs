using Domain.MA;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.MA
{
  public class SPDRepository : DapperRepository<SPD>
  {
    public SPDRepository(IDbConnection connection) : base(connection) { }
    public SPDRepository(IDbConnection connection, ISqlGenerator<SPD> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
