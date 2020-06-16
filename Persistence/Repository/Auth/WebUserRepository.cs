using Dapper;
using Domain.Auth;
using Domain.DM;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repository.Auth
{
  public class WebUserRepository : DapperRepository<WebUser>
  {
    public WebUserRepository(IDbConnection connection) : base(connection) { }
    public WebUserRepository(IDbConnection connection, ISqlGenerator<WebUser> sqlGenerator) : base(connection, sqlGenerator) { }

    public async Task<WebUser> GetUserWithRoleAsync(string userId, long idApp)
    {
      const string cmd = @"SELECT DISTINCT
       w.*,
       w2.*,
       d.*
FROM dbo.WEBUSER w
    INNER JOIN dbo.WEBGROUP w2
        ON w.GROUPID = w2.GROUPID
    INNER JOIN dbo.WEBOTOR w3
        ON w2.GROUPID = w3.GROUPID
    INNER JOIN dbo.WEBROLE w4
        ON w4.ROLEID = w3.ROLEID
    LEFT JOIN dbo.DAFTUNIT d
        ON d.IDUNIT = w.IDUNIT
WHERE w4.IDAPP = @IdApp
      AND w.USERID = @UserId;";

      var user =
        (await Connection.QueryAsync<WebUser, WebGroup, DaftUnit, WebUser>(cmd,
          (webUser, webGroup, unit) =>
          {
            webUser.WebGroup = webGroup;
            if (unit != null) webUser.DaftUnit = unit;
            return webUser;
          }, new { UserId = userId, IdApp = idApp }, splitOn: "GROUPID, IDUNIT"))
        .Distinct()
        .ToList().FirstOrDefault();

      return user;
    }
  }
}
