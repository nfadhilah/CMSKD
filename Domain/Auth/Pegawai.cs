using System.ComponentModel.DataAnnotations;

namespace Domain.Auth
{
  public class Pegawai
  {
    [Key]
    public string Nip { get; set; }

    public string KdGol { get; set; }
    public string UnitKey { get; set; }
    public string Nama { get; set; }
    public string Alamat { get; set; }
    public string Jabatan { get; set; }
    public string Pddk { get; set; }
  }
}