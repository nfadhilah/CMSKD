using System.Collections.Generic;
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

    public WebUserRepository(
      IDbConnection connection, ISqlGenerator<WebUser> sqlGenerator) : base(
      connection, sqlGenerator) { }


    public async Task<IEnumerable<WebUser>> GetUsers(
      long? idApp, long? idUnit, long? groupId,
      List<string> excludedRoleName = null)
    {
      var builder = new SqlBuilder();

      var query = builder.AddTemplate(@"SELECT DISTINCT
       w.*,
       w2.*,
       d.*,
       p.*
FROM dbo.WEBUSER w
    INNER JOIN dbo.WEBGROUP w2
        ON w.GROUPID = w2.GROUPID
    INNER JOIN dbo.WEBOTOR w3
        ON w2.GROUPID = w3.GROUPID
    INNER JOIN dbo.WEBROLE w4
        ON w4.ROLEID = w3.ROLEID
    LEFT JOIN dbo.DAFTUNIT d
        ON d.IDUNIT = w.IDUNIT
    LEFT JOIN PEGAWAI p
        ON p.IDPEG = w.IDPEG
/**where**/");

      if (idApp.HasValue)
        builder.Where("w4.IDAPP = @IdApp", new {IdApp = idApp});

      if (idUnit.HasValue)
        builder.Where("w.IDUNIT = @IdUnit", new {IdUnit = idUnit});

      if (groupId.HasValue)
        builder.Where("w.GROUPID = @GroupId", new {GroupId = groupId});

      if (excludedRoleName != null && excludedRoleName.Any())
        builder.Where("w2.NMGROUP NOT IN @ExcludedRoleName",
          new {ExcludedRoleName = excludedRoleName});

      var user =
        (await Connection
          .QueryAsync<WebUser, WebGroup, DaftUnit, Pegawai, WebUser>(
            query.RawSql,
            (webUser, webGroup, unit, pegawai) =>
            {
              webUser.WebGroup = webGroup;
              if (unit != null) webUser.DaftUnit = unit;
              if (pegawai != null) webUser.Pegawai = pegawai;
              return webUser;
            }, query.Parameters, splitOn: "GROUPID, IDUNIT, IDPEG"))
        .Distinct()
        .ToList();

      return user;
    }

    public async Task<WebUser> GetUserWithRelation(
      string userId, long? idApp = null)
    {
      var builder = new SqlBuilder();

      var cmd = builder.AddTemplate(@"SELECT DISTINCT
       w.*,
       w2.*,
       d.*,
       p.*
FROM dbo.WEBUSER w
    INNER JOIN dbo.WEBGROUP w2
        ON w.GROUPID = w2.GROUPID
    INNER JOIN dbo.WEBOTOR w3
        ON w2.GROUPID = w3.GROUPID
    INNER JOIN dbo.WEBROLE w4
        ON w4.ROLEID = w3.ROLEID
    LEFT JOIN dbo.DAFTUNIT d
        ON d.IDUNIT = w.IDUNIT
    LEFT JOIN PEGAWAI p
        ON p.IDPEG = w.IDPEG
/**where**/;");

      if (!string.IsNullOrWhiteSpace(userId))
        builder.Where("w.USERID = @UserId", new {UserId = userId});

      if (idApp.HasValue)
        builder.Where("w4.IDAPP = @IdApp", new {IdApp = idApp});

      var user =
        (await Connection
          .QueryAsync<WebUser, WebGroup, DaftUnit, Pegawai, WebUser>(
            cmd.RawSql,
            (webUser, webGroup, unit, pegawai) =>
            {
              webUser.WebGroup = webGroup;
              if (unit != null) webUser.DaftUnit = unit;
              if (pegawai != null) webUser.Pegawai = pegawai;
              return webUser;
            }, new {UserId = userId, IdApp = idApp},
            splitOn: "GROUPID, IDUNIT, IDPEG"))
        .Distinct()
        .ToList().FirstOrDefault();

      return user;
    }
  }
}