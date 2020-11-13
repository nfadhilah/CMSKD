using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.TU
{
  [Table("SP2DDETRTL")]
  public class SP2DDetRTL
  {
    [Key]
    public string MtgKey { get; set; }
    [Key]
    public string UnitKey { get; set; }
    [Key]
    public string NoSP2D { get; set; }
    [Key]
    public string NoJeTra { get; set; }
    public string KdDana { get; set; }
    public decimal? Nilai { get; set; }
  }
}