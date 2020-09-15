using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Domain.Auth;
using Domain.DM;
using Domain.PM;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Common;

namespace Persistence.Repository.PM
{
  public class UserKegiatanRepository : CommonRepository<UserKegiatan>
  {
    public UserKegiatanRepository(IDbConnection connection) :
      base(connection) { }

    public UserKegiatanRepository(
      IDbConnection connection, ISqlGenerator<UserKegiatan> sqlGenerator) :
      base(connection, sqlGenerator) { }

    public async Task<IEnumerable<UserKegiatan>> GetListUserKegiatan(
      string userId, List<long> listIdKeg = null)
    {
      var builder = new SqlBuilder();

      var cmd =
        builder.AddTemplate(
          @"SELECT u.*, w.*, m.* FROM USERKEGIATAN as u 
LEFT JOIN WEBUSER as w ON u.USERID = w.USERID 
LEFT JOIN MKEGIATAN as m ON u.IDKEG = m.IDKEG
/**where**/");

      if (!string.IsNullOrWhiteSpace(userId))
        builder.Where("u.USERID = @UserId", new {UserId = userId});

      if (listIdKeg != null && listIdKeg.Any())
        builder.Where("u.IDKEG IN @ListIdKeg", new {ListIdKeg = listIdKeg});

      return await Connection
        .QueryAsync<UserKegiatan, WebUser, MKegiatan, UserKegiatan>(
          cmd.RawSql,
          (u, w, m) =>
          {
            if (w != null) u.WebUser = w;
            if (m != null) u.MKegiatan = m;
            return u;
          }, cmd.Parameters, splitOn: "USERID, IDKEG");
    }


    public async Task<int> DeleteByUserIdAndListIdKeg(
      string userId, List<long> listIdKeg)
    {
      return await Connection.ExecuteAsync(
        "DELETE FROM USERKEG WHERE USERID = @UserId AND IDKEG IN @ListIdKeg",
        new {UserId = userId, ListIdKeg = listIdKeg});
    }
  }
}