using Domain.TUBEND;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.TUBEND
{
  public class SPMRepository : DapperRepository<SPM>
  {
    public SPMRepository(IDbConnection connection) : base(connection) { }
    public SPMRepository(IDbConnection connection, ISqlGenerator<SPM> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
