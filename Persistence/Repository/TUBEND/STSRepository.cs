using Domain.TUBEND;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.TUBEND
{
  public class STSRepository : DapperRepository<STS>
  {
    public STSRepository(IDbConnection connection) : base(connection) { }
    public STSRepository(IDbConnection connection, ISqlGenerator<STS> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
