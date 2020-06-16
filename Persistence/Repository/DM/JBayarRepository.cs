using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.DM
{
  public class JBayarRepository : DapperRepository<JBayar>
  {
    public JBayarRepository(IDbConnection connection) : base(connection)
    {
    }

    public JBayarRepository(
      IDbConnection connection, ISqlGenerator<JBayar> sqlGenerator) : base(
      connection, sqlGenerator)
    {
    }
  }
}