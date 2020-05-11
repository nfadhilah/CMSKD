using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
  [Table("DAFTPHK3")]
  public class DaftPhk3
  {
    [Key]
    public string KdP3 { get; set; }
    public string NmP3 { get; set; }
    public string NmInst { get; set; }
    public string NoRcP3 { get; set; }
    public string NmBank { get; set; }
    public string JnsUsaha { get; set; }
    public string Alamat { get; set; }
    public string Telepon { get; set; }
    public string NPWP { get; set; }
    public string UnitKey { get; set; }
  }
}