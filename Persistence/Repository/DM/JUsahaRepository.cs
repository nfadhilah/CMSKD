using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.DM
{
  public class JUsahaRepository : DapperRepository<JUsaha>
  {
    public JUsahaRepository(IDbConnection connection) : base(connection) { }
    public JUsahaRepository(IDbConnection connection, ISqlGenerator<JUsaha> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
