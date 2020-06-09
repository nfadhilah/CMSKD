using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
{
  public class JTransRepository : DapperRepository<JTrans>
  {
    public JTransRepository(IDbConnection connection) : base(connection)
    {
    }

    public JTransRepository(
      IDbConnection connection, ISqlGenerator<JTrans> sqlGenerator) : base(
      connection, sqlGenerator)
    {
    }
  }
}