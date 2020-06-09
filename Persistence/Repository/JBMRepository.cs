using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
{
  public class JBMRepository : DapperRepository<JBM>
  {
    public JBMRepository(IDbConnection connection) : base(connection)
    {
    }

    public JBMRepository(
      IDbConnection connection, ISqlGenerator<JBM> sqlGenerator) : base(
      connection, sqlGenerator)
    {
    }
  }
}