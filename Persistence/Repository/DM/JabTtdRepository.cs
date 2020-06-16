using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.DM
{
  public class JabTtdRepository : DapperRepository<JabTtd>

  {
    public JabTtdRepository(IDbConnection connection) : base(connection) { }
    public JabTtdRepository(IDbConnection connection, ISqlGenerator<JabTtd> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
