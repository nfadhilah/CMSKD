using Domain.Auth;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.Auth
{
  public class WebRoleRepository : DapperRepository<WebRole>
  {
    public WebRoleRepository(IDbConnection connection) : base(connection) { }
    public WebRoleRepository(IDbConnection connection, ISqlGenerator<WebRole> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
