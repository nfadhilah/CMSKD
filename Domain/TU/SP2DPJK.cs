using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.TU
{
  [Table("SP2DPJK")]
  public class SP2DPJK
  {
    [Key]
    public string UnitKey { get; set; }
    [Key]
    public string NoSP2D { get; set; }
    [Key]
    public string PjkKey { get; set; }
    public decimal? Nilai { get; set; }
    public string Keterangan { get; set; }
  }
}