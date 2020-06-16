using Domain.Auth;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;

namespace Persistence.Repository.Auth
{
  public class WebGroupRepository : DapperRepository<WebGroup>
  {
    public WebGroupRepository(IDbConnection connection) : base(connection) { }
    public WebGroupRepository(IDbConnection connection, ISqlGenerator<WebGroup> sqlGenerator) : base(connection, sqlGenerator) { }
  }
}
