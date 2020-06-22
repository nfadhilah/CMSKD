using Domain.MA;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.MA
{
  public class SPDDetBRepository : DapperRepository<SPDDetB>
  {
    public SPDDetBRepository(IDbConnection connection) : base(connection) { }
    public SPDDetBRepository(IDbConnection connection, ISqlGenerator<SPDDetB> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
