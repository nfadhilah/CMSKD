using Domain.DM;
using Domain.MA;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.BUD
{
  public class SP2D
  {
    [Key, Identity]
    public long IdSP2D { get; set; }
    public string NoSP2D { get; set; }
    public long IdUnit { get; set; }
    [InnerJoin("DAFTUNIT", "IDUNIT", "IDUNIT")]
    public DaftUnit Unit { get; set; }
    public string KdStatus { get; set; }
    public long IdSPM { get; set; }
    public string NoSPM { get; set; }
    public long? IdBend { get; set; }
    [LeftJoin("BEND", "IDBEND", "IDBEND")]
    public Bend Bend { get; set; }
    public long? IdSPD { get; set; }
    [LeftJoin("SPD", "IDSPD", "IDSPD")]
    public SPD SPD { get; set; }
    public long? IdPhk3 { get; set; }
    [LeftJoin("DAFTPHK3", "IDPHK3", "IDPHK3")]
    public DaftPhk3 Phk3 { get; set; }
    public long? IdTtd { get; set; }
    [LeftJoin("JABTTD", "IDTTD", "IDTTD")]
    public JabTtd JabTtd { get; set; }
    public int? IdxKode { get; set; }
    public string NoReg { get; set; }
    public string KetOtor { get; set; }
    public string NoKontrak { get; set; }
    public string Keperluan { get; set; }
    public string Penolakan { get; set; }
    public DateTime? TglValid { get; set; }
    public DateTime? TglSP2D { get; set; }
    public DateTime? TglSPM { get; set; }
    public string NoBBantu { get; set; }
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}
