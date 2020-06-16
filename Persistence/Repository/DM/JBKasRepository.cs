using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.DM
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