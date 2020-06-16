using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.DM
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