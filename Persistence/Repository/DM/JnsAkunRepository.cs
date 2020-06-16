using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.DM
{
  public class JnsAkunRepository : DapperRepository<JnsAkun>
  {
    public JnsAkunRepository(IDbConnection connection) : base(connection)
    {
    }

    public JnsAkunRepository(
      IDbConnection connection, ISqlGenerator<JnsAkun> sqlGenerator) : base(
      connection, sqlGenerator)
    {
    }
  }
}