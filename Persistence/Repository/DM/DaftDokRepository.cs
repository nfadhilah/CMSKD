using Domain.DM;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Common;
using System.Data;

namespace Persistence.Repository.DM
{
  public class DaftDokRepository : CommonRepository<DaftDok>
  {
    public DaftDokRepository(IDbConnection connection) : base(connection)
    {
    }

    public DaftDokRepository(IDbConnection connection, ISqlGenerator<DaftDok> sqlGenerator) : base(connection, sqlGenerator)
    {
    }
  }
}
