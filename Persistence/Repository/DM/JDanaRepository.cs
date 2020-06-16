using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.DM
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