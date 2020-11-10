using System.Data;
using Domain.Auth;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Common;

namespace Persistence.Repository.Auth
{
  public class WebRoleRepository : CommonRepository<WebRole>
  {
    public WebRoleRepository(IDbConnection connection) : base(connection)
    {
    }

    public WebRoleRepository(IDbConnection connection, ISqlGenerator<WebRole> sqlGenerator) : base(connection,
      sqlGenerator)
    {
    }
  }
}