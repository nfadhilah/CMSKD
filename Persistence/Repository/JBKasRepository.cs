using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
{
  public class JBKasRepository : DapperRepository<JBKas>
  {
    public JBKasRepository(IDbConnection connection) : base(connection)
    {
    }

    public JBKasRepository(
      IDbConnection connection, ISqlGenerator<JBKas> sqlGenerator) : base(
      connection, sqlGenerator)
    {
    }
  }
}