using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
  [Table("DAFTUNIT")]
  public class DaftUnit
  {
    [Key]
    public string UnitKey { get; set; }
    public int KdLevel { get; set; }
    public string KdUnit { get; set; }
    public string NmUnit { get; set; }
    public string AkroUnit { get; set; }
    public string Alamat { get; set; }
    public string Telepon { get; set; }
    public string Type { get; set; }
  }
}
