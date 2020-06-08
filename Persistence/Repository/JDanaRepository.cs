using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
{
  public class JDanaRepository : DapperRepository<JDana>
  {
    public JDanaRepository(IDbConnection connection) : base(connection)
    {
    }

    public JDanaRepository(
      IDbConnection connection, ISqlGenerator<JDana> sqlGenerator) : base(
      connection, sqlGenerator)
    {
    }
  }
}