using Dapper;
using Domain.DM;
using Domain.TUBEND;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Persistence.Repository.TUBEND
{
  public interface ITagihanSqlParams
  {
    public long? IdUnit { get; set; }
    public long? IdKeg { get; set; }
    public string NoTagihan { get; set; }
    public DateTime? TglTagihan { get; set; }
    public long? IdKontrak { get; set; }
    public string UraianTagihan { get; set; }
    public DateTime? TglValid { get; set; }
    public string KdStatus { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }

  public class TagihanRepository : CommonRepository<Tagihan>
  {
    public TagihanRepository(IDbConnection connection) : base(connection)
    {
    }

    public TagihanRepository(IDbConnection connection, ISqlGenerator<Tagihan> sqlGenerator) : base(connection, sqlGenerator)
    {
    }

    public async Task<IEnumerable<Tagihan>> GetAllAsync(ITagihanSqlParams parameters, uint limit, uint offset)
    {
      var builder = new SqlBuilder();

      var cmd = builder.AddTemplate(@"SELECT t.IDTAGIHAN,
       t.IDUNIT,
       t.IDKEG,
       t.NOTAGIHAN,
       t.TGLTAGIHAN,
       t.IDKONTRAK,
       t.URAIANTAGIHAN,
       t.TGLVALID,
       t.KDSTATUS,
       t.DATECREATE,
       t.DATEUPDATE,  
       SUM(t2.NILAI)         AS JUMLAH,
       k.IDKONTRAK,
       k.NOKONTRAK,
       d.IDPHK3,
       d.NMPHK3
FROM   TAGIHAN               AS t
       LEFT JOIN KONTRAK     AS k
            ON  k.IDKONTRAK = t.IDKONTRAK
       LEFT JOIN DAFTPHK3    AS d
            ON  k.IDPHK3 = d.IDPHK3
       LEFT JOIN TAGIHANDET  AS t2
            ON  t2.IDTAGIHAN = t.IDTAGIHAN
/**where**/
GROUP BY
       t.IDTAGIHAN,
       t.IDUNIT,
       t.IDKEG,
       t.NOTAGIHAN,
       t.TGLTAGIHAN,
       t.IDKONTRAK,
       t.URAIANTAGIHAN,
       t.TGLVALID,
       t.KDSTATUS,
       t.DATECREATE,
       t.DATEUPDATE,
       k.IDKONTRAK,
       k.NOKONTRAK,
       d.IDPHK3,
       d.NMPHK3
");

      if (parameters.IdUnit.HasValue)
        builder.Where("t.IDUNIT = @IdUnit", new { parameters.IdUnit });

      if (parameters.IdKeg.HasValue)
        builder.Where("t.IDKEG = @IdKeg", new { parameters.IdKeg });

      if (!string.IsNullOrWhiteSpace(parameters.NoTagihan))
        builder.Where("t.IDKEG LIKE @NoTagihan", new { IdKeg = $"%{parameters.NoTagihan}%" });

      if (parameters.TglTagihan.HasValue)
        builder.Where("t.TGLTAGIHAN = @TglTagihan", new { parameters.TglTagihan });

      if (parameters.IdKontrak.HasValue)
        builder.Where("t.IDKONTRAK = @IdKontrak", new { parameters.IdKontrak });

      if (!string.IsNullOrWhiteSpace(parameters.UraianTagihan))
        builder.Where("t.URAITAGIHAN LIKE @UraiTagihan", new { IdKeg = $"%{parameters.UraianTagihan}%" });

      if (parameters.TglValid.HasValue)
        builder.Where("t.TGLVALID = @TglValid", new { parameters.TglValid });

      if (!string.IsNullOrWhiteSpace(parameters.KdStatus))
        builder.Where("t.KDSTATUS = @KdStatus", new { parameters.KdStatus });

      var paginatedQuery = PaginatedQueryBuilder(cmd, offset, limit,
        new List<string> { "t.NOTAGIHAN ASC" });

      return await Connection.QueryAsync<Tagihan, Kontrak, DaftPhk3, Tagihan>(paginatedQuery.RawSql,
        (tagihan, kontrak, phk3) =>
        {
          tagihan.Kontrak = kontrak;
          tagihan.DaftPhk3 = phk3;

          return tagihan;
        }, cmd.Parameters, splitOn: "IDKONTRAK, IDPHK3");
    }
  }
}
