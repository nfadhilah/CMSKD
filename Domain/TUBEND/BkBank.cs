using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.TUBEND
{
  [Table("BKBANK")]
  public class BkBank
  {
    [Key, Identity]
    public long IdBkBank { get; set; }
    public long IdUnit { get; set; }
    [InnerJoin("DAFTUNIT", "IDUNIT", "IDUNIT")]
    public DaftUnit Unit { get; set; }
    public long IdBend { get; set; }
    [InnerJoin("BEND", "IDBEND", "IDBEND")]
    public Bend Bend { get; set; }
    public string NoBuku { get; set; }
    public string KdStatus { get; set; }
    [InnerJoin("STATTRS", "KDSTATUS", "KDSTATUS")]
    public StatTrs StatTrs { get; set; }
    public DateTime? TglBuku { get; set; }
    public string Uraian { get; set; }
    public DateTime? TglValid { get; set; }
  }
}
