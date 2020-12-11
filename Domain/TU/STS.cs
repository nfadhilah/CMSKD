using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.TU
{
  [Table("STS")]
  public class STS
  {
    [Key]
    public string UnitKey { get; set; }
    [Key]
    public string NoSTS { get; set; }
    public string KeyBend1 { get; set; }
    public string KdStatus { get; set; }
    public string IdXKode { get; set; }
    public string KeyBend2 { get; set; }
    public string IdxTtd { get; set; }
    public string NoBBantu { get; set; }
    public DateTime? TglSts { get; set; }
    public string Uraian { get; set; }
    public DateTime? TglValid { get; set; }
  }
}
