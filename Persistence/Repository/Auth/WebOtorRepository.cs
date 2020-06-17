using Domain.Auth;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.Auth
{
  public class WebOtorRepository : DapperRepository<WebOtor>
  {
    public WebOtorRepository(IDbConnection connection) : base(connection) { }
    public WebOtorRepository(IDbConnection connection, ISqlGenerator<WebOtor> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
