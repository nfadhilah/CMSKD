using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.TUBEND
{
  [Table("BKPAJAK")]
  public class BkPajak
  {
    [Key, Identity]
    public long IdBkPajak { get; set; }
    public long IdUnit { get; set; }
    [InnerJoin("DAFTUNIT", "IDUNIT", "IDUNIT")]
    public DaftUnit Unit { get; set; }
    public long IdBend { get; set; }
    [InnerJoin("BEND", "IDBEND", "IDBEND")]
    public Bend Bend { get; set; }
    public string NoBkPajak { get; set; }
    public int IdxKode { get; set; }
    public string KdStatus { get; set; }
    public DateTime? TglBkPajak { get; set; }
    public string Uraian { get; set; }
    public DateTime? TglValid { get; set; }
    public long? KdRilis { get; set; }
    public int? StKirim { get; set; }
    public int? StCair { get; set; }
    public int IdTransfer { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}