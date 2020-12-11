using Domain.TU;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Common;
using System.Data;

namespace Persistence.Repository.TU
{
  public class STSRepository : CommonRepository<STS>
  {
    public STSRepository(IDbConnection connection) : base(connection)
    {
    }

    public STSRepository(IDbConnection connection, ISqlGenerator<STS> sqlGenerator) : base(connection, sqlGenerator)
    {
    }
  }
}
