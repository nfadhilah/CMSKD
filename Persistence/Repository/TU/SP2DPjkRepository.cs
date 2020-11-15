using Domain.TU;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Common;
using System.Data;

namespace Persistence.Repository.TU
{
  public class SP2DPjkRepository : CommonRepository<SP2DPjk>
  {
    public SP2DPjkRepository(IDbConnection connection) : base(connection)
    {
    }

    public SP2DPjkRepository(IDbConnection connection, ISqlGenerator<SP2DPjk> sqlGenerator) : base(connection, sqlGenerator)
    {
    }
  }
}
