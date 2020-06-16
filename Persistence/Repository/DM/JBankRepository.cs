using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.DM
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