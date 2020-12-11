using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.TU
{
  [Table("RKMDETD")]
  public class RkmDetD
  {
    [Key]
    public string MtgKey { get; set; }
    [Key]
    public string UnitKey { get; set; }
    [Key]
    public string NoSTS { get; set; }
    [Key]
    public string NoJeTra { get; set; }
    public decimal? Nilai { get; set; }
  }
}