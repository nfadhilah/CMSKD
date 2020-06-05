using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
{
  public class JBankRepository : DapperRepository<JBank>
  {
    public JBankRepository(IDbConnection connection) : base(connection)
    {
    }

    public JBankRepository(
      IDbConnection connection, ISqlGenerator<JBank> sqlGenerator) : base(
      connection, sqlGenerator)
    {
    }
  }
}