using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.DM
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