using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
{
  public class JabTtdRepository : DapperRepository<JabTtd>

  {
    public JabTtdRepository(IDbConnection connection) : base(connection) { }
    public JabTtdRepository(IDbConnection connection, ISqlGenerator<JabTtd> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
