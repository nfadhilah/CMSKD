using Domain.DM;
using Domain.MA;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.TUBEND
{
  [Table("SPP")]
  public class SPP
  {
    [Key, Identity]
    public long IdSPP { get; set; }
    public long IdUnit { get; set; }
    [InnerJoin("DAFTUNIT", "IDUNIT", "IDUNIT")]
    public DaftUnit Unit { get; set; }
    public string NoSPP { get; set; }
    public string KdStatus { get; set; }
    [InnerJoin("STATTRS", "KDSTATUS", "KDSTATUS")]
    public StatTrs StatTrs { get; set; }
    public int KdBulan { get; set; }
    public long? IdBend { get; set; }
    [LeftJoin("BEND", "IDBEND", "IDBEND")]
    public Bend Bendahara { get; set; }
    public long IdSPD { get; set; }
    [InnerJoin("SPD", "IDSPD", "IDSPD")]
    public SPD SPD { get; set; }
    public long? IdPhk3 { get; set; }
    [LeftJoin("DAFTPHK3", "IDPHK3", "IDPHK3")]
    public DaftPhk3 Phk3 { get; set; }
    public int IdxKode { get; set; }
    [InnerJoin("ZKODE", "IDXKODE", "IDXKODE")]
    public ZKode ZKode { get; set; }
    public string NoReg { get; set; }
    public string KetOtor { get; set; }
    public string NoKontrak { get; set; }
    public string Keperluan { get; set; }
    public string Penolakan { get; set; }
    public DateTime? TglValid { get; set; }
    public DateTime? TglSPP { get; set; }
    public DateTime? Status { get; set; }
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}
