using Domain.DM;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Common;
using System.Data;

namespace Persistence.Repository.DM
{
  public class JTermOrLunRepository : CommonRepository<JTermOrLun>
  {
    public JTermOrLunRepository(IDbConnection connection) : base(connection)
    {
    }

    public JTermOrLunRepository(IDbConnection connection, ISqlGenerator<JTermOrLun> sqlGenerator) : base(connection, sqlGenerator)
    {
    }
  }
}
