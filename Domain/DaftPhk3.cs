using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
  [Table("DAFTPHK3")]
  public class DaftPhk3
  {
    [Key]
    public string Kdp3 { get; set; }
    public string Nmp3 { get; set; }
    public string Nminst { get; set; }
    public string Norcp3 { get; set; }
    public string Nmbank { get; set; }
    public string Jnsusaha { get; set; }
    public string Alamat { get; set; }
    public string Telepon { get; set; }
    public string NPWP { get; set; }
    public string Unitkey { get; set; }
  }
}