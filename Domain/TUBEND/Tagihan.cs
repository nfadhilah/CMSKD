using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.TUBEND
{
  [Table("TAGIHAN")]
  public class Tagihan
  {
    [Key, Identity]
    public long IdTagihan { get; set; }
    public long IdUnit { get; set; }
    public long IdKeg { get; set; }
    public string NoTagihan { get; set; }
    public DateTime TglTagihan { get; set; }
    public long IdKontrak { get; set; }
    [InnerJoin("KONTRAK", "IDKONTRAK", "IDKONTRAK")]
    public Kontrak Kontrak { get; set; }
    public string UraianTagihan { get; set; }
    public DateTime? TglValid { get; set; }
    public string KdStatus { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
