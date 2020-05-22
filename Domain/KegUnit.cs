using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
  [Table("KEGUNIT")]
  public class KegUnit
  {
    [Key]
    public int KdTahap { get; set; }

    [Key]
    public string UnitKey { get; set; }

    [Key]
    public string KdKegUnit { get; set; }

    [Key]
    public string IdPrgrm { get; set; }

    public string NoPrior { get; set; }
    public string KdSifat { get; set; }
    public string NIP { get; set; }
    public DateTime? TglAwal { get; set; }
    public DateTime? TglAkhir { get; set; }
    public decimal TargetP { get; set; }
    public string Lokasi { get; set; }
    public decimal JumlahMin1 { get; set; }
    public decimal Pagu { get; set; }
    public decimal JumlahPls1 { get; set; }
    public string Sasaran { get; set; }
    public string KetKeg { get; set; }
  }
}