using Domain.TUBEND;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Common;
using System.Data;

namespace Persistence.Repository.TUBEND
{
  public class BkBankDetRepository : CommonRepository<BkBankDet>
  {
    public BkBankDetRepository(IDbConnection connection) : base(connection) { }
    public BkBankDetRepository(IDbConnection connection, ISqlGenerator<BkBankDet> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}