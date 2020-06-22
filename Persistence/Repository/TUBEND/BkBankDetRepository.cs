using Domain.TUBEND;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.TUBEND
{
  public class BkBankDetRepository : DapperRepository<BkBankDet>
  {
    public BkBankDetRepository(IDbConnection connection) : base(connection) { }
    public BkBankDetRepository(IDbConnection connection, ISqlGenerator<BkBankDet> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}