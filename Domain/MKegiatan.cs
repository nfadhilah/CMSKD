using System.ComponentModel.DataAnnotations;

namespace Domain
{
  public class MKegiatan
  {
    [Key]
    public string KdKegUnit { get; set; }
    public string IdPrgrm { get; set; }
    public string KdPerspektif { get; set; }
    public string NuKeg { get; set; }
    public string NmKegUnit { get; set; }
    public string LevelKeg { get; set; }
    public string Type { get; set; }
  }
}
