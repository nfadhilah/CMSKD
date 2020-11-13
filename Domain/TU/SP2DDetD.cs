using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.TU
{
  [Table("SP2DDETD")]
  public class SP2DDetD
  {
    [Key]
    public string MtgKey { get; set; }
    [Key]
    public string UnitKey { get; set; }
    [Key]
    public string NoSP2D { get; set; }
    [Key]
    public string NoJeTra { get; set; }
    public decimal? Nilai { get; set; }
  }
}