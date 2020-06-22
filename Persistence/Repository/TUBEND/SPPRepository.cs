using Domain.TUBEND;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.TUBEND
{
  public class SPPRepository : DapperRepository<SPP>
  {
    public SPPRepository(IDbConnection connection) : base(connection) { }
    public SPPRepository(IDbConnection connection, ISqlGenerator<SPP> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
