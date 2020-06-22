using Domain.MA;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.MA
{
  public class SPDDetRRepository : DapperRepository<SPDDetR>
  {
    public SPDDetRRepository(IDbConnection connection) : base(connection) { }
    public SPDDetRRepository(IDbConnection connection, ISqlGenerator<SPDDetR> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}