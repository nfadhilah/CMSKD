using System.Data;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;

namespace Persistence.Repository.Auth
{
  public class WebUserRepository : DapperRepository<WebUser>
  {
    public WebUserRepository(IDbConnection connection) : base(connection)
    {
    }

    public WebUserRepository(IDbConnection connection, ISqlGenerator<WebUser> sqlGenerator) : base(connection, sqlGenerator)
    {
    }
  }
}
