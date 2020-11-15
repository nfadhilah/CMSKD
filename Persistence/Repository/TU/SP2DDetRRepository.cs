using Domain.TU;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Common;
using System.Data;

namespace Persistence.Repository.TU
{
  public class SP2DDetRRepository : CommonRepository<SP2DDetR>
  {
    public SP2DDetRRepository(IDbConnection connection) : base(connection)
    {
    }

    public SP2DDetRRepository(IDbConnection connection, ISqlGenerator<SP2DDetR> sqlGenerator) : base(connection, sqlGenerator)
    {
    }
  }
}
