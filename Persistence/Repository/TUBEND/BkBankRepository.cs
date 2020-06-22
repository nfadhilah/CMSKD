using Domain.TUBEND;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.TUBEND
{
  public class BkBankRepository : DapperRepository<BkBank>
  {
    public BkBankRepository(IDbConnection connection) : base(connection) { }
    public BkBankRepository(IDbConnection connection, ISqlGenerator<BkBank> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
