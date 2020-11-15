using Domain.TU;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Common;
using System.Data;

namespace Persistence.Repository.TU
{
  public class SP2DDetBRepository : CommonRepository<SP2DDetB>
  {
    public SP2DDetBRepository(IDbConnection connection) : base(connection)
    {
    }

    public SP2DDetBRepository(IDbConnection connection, ISqlGenerator<SP2DDetB> sqlGenerator) : base(connection, sqlGenerator)
    {
    }
  }
}
