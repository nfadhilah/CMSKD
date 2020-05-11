using Domain;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository
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
