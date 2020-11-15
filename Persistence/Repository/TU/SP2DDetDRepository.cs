using Domain.TU;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Common;
using System.Data;

namespace Persistence.Repository.TU
{
  public class SP2DDetDRepository : CommonRepository<SP2DDetD>
  {
    public SP2DDetDRepository(IDbConnection connection) : base(connection)
    {
    }

    public SP2DDetDRepository(IDbConnection connection, ISqlGenerator<SP2DDetD> sqlGenerator) : base(connection, sqlGenerator)
    {
    }
  }
}
