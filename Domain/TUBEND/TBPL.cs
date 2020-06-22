using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.TUBEND
{
  [Table("TBPL")]
  public class TBPL
  {
    [Key, Identity]
    public long IdTBPL { get; set; }
    public long IdUnit { get; set; }
    [InnerJoin("DAFTUNIT", "IDUNIT", "IDUNIT")]
    public DaftUnit Unit { get; set; }
    public string NoTBPL { get; set; }
    public long IdBend { get; set; }
    [LeftJoin("BEND", "IDBEND", "IDBEND")]
    public Bend Bend { get; set; }
    public string KdStatus { get; set; }
    [InnerJoin("STATTRS", "KDSTATUS", "KDSTATUS")]
    public StatTrs StatTrs { get; set; }
    public int IdxKode { get; set; }
    [InnerJoin("ZKODE", "IDXKODE", "IDXKODE")]
    public ZKode ZKode { get; set; }
    public DateTime? TglTBPL { get; set; }
    public string Penyetor { get; set; }
    public string Alamat { get; set; }
    public string UraiTBPL { get; set; }
    public DateTime? TglValid { get; set; }
    public long? KdRilis { get; set; }
    public int? StKirim { get; set; }
    public int? StCair { get; set; }
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}
