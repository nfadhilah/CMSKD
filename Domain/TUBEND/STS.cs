using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.TUBEND
{
  [Table("STS")]
  public class STS
  {
    [Key, Identity]
    public long IdSTS { get; set; }
    public long IdUnit { get; set; }
    [InnerJoin("DAFTUNIT", "IDUNIT", "IDUNIT")]
    public DaftUnit Unit { get; set; }
    public string NoSTS { get; set; }
    public long IdBend { get; set; }
    [LeftJoin("BEND", "IDBEND", "IDBEND")]
    public Bend Bend { get; set; }
    public string KdStatus { get; set; }
    public int IdxKode { get; set; }
    public long IdKas { get; set; }
    public DateTime? TglSTS { get; set; }
    public string Uraian { get; set; }
    public DateTime? TglValid { get; set; }
    public long? KdRilis { get; set; }
    public int? StKirim { get; set; }
    public int? StCair { get; set; }
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}
