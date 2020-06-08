using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
{
  public class JBendRepository : DapperRepository<JBend>
  {
    public JBendRepository(IDbConnection connection) : base(connection)
    {
    }

    public JBendRepository(
      IDbConnection connection, ISqlGenerator<JBend> sqlGenerator) : base(
      connection, sqlGenerator)
    {
    }
  }
}