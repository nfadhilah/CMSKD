using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.DM
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