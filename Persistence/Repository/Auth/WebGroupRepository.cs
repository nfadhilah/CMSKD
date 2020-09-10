using System.Collections.Generic;
using Domain.Auth;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Persistence.Repository.Common;

namespace Persistence.Repository.Auth
{
  public class WebGroupRepository : CommonRepository<WebGroup>
  {
    public WebGroupRepository(IDbConnection connection) : base(connection) { }

    public WebGroupRepository(
      IDbConnection connection, ISqlGenerator<WebGroup> sqlGenerator) : base(
      connection, sqlGenerator) { }


    public async Task<IEnumerable<WebGroup>> GetListWebGroup(long? idApp)
    {
      var builder = new SqlBuilder();

      var query = builder.AddTemplate(@"SELECT DISTINCT w.* FROM dbo.WEBGROUP w
LEFT JOIN dbo.WEBOTOR w2
ON w2.GROUPID = w.GROUPID
LEFT JOIN dbo.WEBROLE w3
ON w3.ROLEID = w2.ROLEID
/**where**/");

      if (idApp.HasValue)
        builder.Where("w3.IDAPP = @IdApp", new {IdApp = idApp});

      return await Connection.QueryAsync<WebGroup>(query.RawSql,
        query.Parameters);
    }
  }
}