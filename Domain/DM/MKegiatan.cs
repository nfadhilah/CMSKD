using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DM
{
  [Table("MKEGIATAN")]
  public class MKegiatan
  {
    [Key]
    public string KdKegUnit { get; set; }
    public string IdPrgrm { get; set; }
    public string KdPerspektif { get; set; }
    public string Nukeg { get; set; }
    public string NmKegUnit { get; set; }
    public string LevelKeg { get; set; }
    public string Type { get; set; }
    public string Kd_KegInduk { get; set; }
  }
}
