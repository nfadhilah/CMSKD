using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
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