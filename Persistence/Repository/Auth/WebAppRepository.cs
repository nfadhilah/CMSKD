using Domain.Auth;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.Auth
{
  public class WebAppRepository : DapperRepository<WebApp>
  {
    public WebAppRepository(IDbConnection connection) : base(connection) { }
    public WebAppRepository(IDbConnection connection, ISqlGenerator<WebApp> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
