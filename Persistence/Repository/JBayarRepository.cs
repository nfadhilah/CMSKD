using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
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