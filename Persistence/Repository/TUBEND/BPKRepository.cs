using Domain.TUBEND;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.TUBEND
{
  public class BPKRepository : DapperRepository<BPK>
  {
    public BPKRepository(IDbConnection connection) : base(connection) { }
    public BPKRepository(IDbConnection connection, ISqlGenerator<BPK> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
