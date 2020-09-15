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

    public async Task<IEnumerable<dynamic>> GetTreeUserKegiatan(
      long idUnit, int kdTahap, string userId, bool? isSelected = null)
    {
      var builder = new SqlBuilder();

      var cmd = builder.AddTemplate(
        @"SELECT DISTINCT b.Lvl, b.Kode, b.Label, b.IdKeg, b.UserId, b.PPK as Nama, b.IsSelected, b.[Type]  FROM
(
	SELECT m.IDKEG,
		   RTRIM(d.KDURUS) AS KDURUS,
		   d.NMURUS,
		   RTRIM(d.KDURUS) + RTRIM(m3.NUPRGRM) AS NUPRGRM,
		   m3.NMPRGRM,
		   RTRIM(d.KDURUS) + RTRIM(m3.NUPRGRM) + RTRIM(m2.NUKEG) AS NUKEG,
		   m2.NMKEGUNIT,
		   RTRIM(d.KDURUS) + RTRIM(m3.NUPRGRM) + RTRIM(m2.NUKEG) + RTRIM(m.NUKEG) AS NUSUBKEG,
		   m.NMKEGUNIT AS NMSUBKEG,
		   w.USERID,
		   p.NIP AS NIPPPK,
		   p.NAMA AS PPK,
		   CASE WHEN u.USERID IS NULL THEN 0 ELSE 1
		   END AS ISSELECTED
	FROM dbo.KEGUNIT k
		LEFT JOIN dbo.USERKEGIATAN u
			ON u.IDKEG = k.IDKEG
		LEFT JOIN dbo.MKEGIATAN m
			ON m.IDKEG = k.IDKEG
		LEFT JOIN dbo.MKEGIATAN m2
			ON m2.IDKEG = m.IDKEGINDUK
		LEFT JOIN dbo.WEBUSER w
			ON w.USERID = u.USERID
		LEFT JOIN dbo.WEBGROUP w2
			ON w2.GROUPID = w.GROUPID
		LEFT JOIN dbo.MPGRM m3
				ON m3.IDPRGRM = m.IDPRGRM
			LEFT JOIN dbo.DAFTURUS d
				ON d.IDURUS = m3.IDURUS
		LEFT JOIN dbo.PEGAWAI p
			ON p.IDPEG = w.IDPEG
	/**where**/
) AS a
CROSS APPLY
(
	SELECT 1, a.KDURUS, a.NMURUS, NULL, NULL, NULL, NULL, 'H'
	UNION ALL 
	SELECT 2, a.NUPRGRM, a.NMPRGRM, NULL, NULL, NULL, NULL,  'H'
	UNION ALL
	SELECT 3, a.NUKEG, a.NMKEGUNIT, NULL, NULL, NULL, NULL, 'H'
	UNION ALL
	SELECT 4, a.NUSUBKEG, a.NMSUBKEG, a.IDKEG, a.USERID, a.PPK, a.ISSELECTED, 'D'
) AS b (Lvl, Kode, Label, IdKeg, UserId, PPK, IsSelected, [Type])
ORDER BY b.Kode");

      builder.Where("k.IDUNIT = @IdUnit AND k.KDTAHAP = @KdTahap",
        new {IdUnit = idUnit, KdTahap = kdTahap});

      if (!string.IsNullOrWhiteSpace(userId))
        builder.Where("w.USERID = @UserId", new {UserId = userId});

      switch (isSelected)
      {
        case true:
          builder.Where("w.USERID IS NOT NULL");
          break;
        case false:
          builder.Where("w.USERID IS NULL");
          break;
      }

      return await Connection.QueryAsync(cmd.RawSql, cmd.Parameters);
    }
  }
}