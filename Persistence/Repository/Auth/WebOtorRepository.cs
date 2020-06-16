using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.Auth
{
  public class WebOtorRepository : DapperRepository<WebOtorRepository>
  {
    public WebOtorRepository(IDbConnection connection) : base(connection) { }
    public WebOtorRepository(IDbConnection connection, ISqlGenerator<WebOtorRepository> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
