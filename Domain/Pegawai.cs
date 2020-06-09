using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
  [Table("PEGAWAI")]
  public class Pegawai
  {
    [Key]
    public string IdPeg { get; set; }
    public string NIP { get; set; }
    public string IdUnit { get; set; }
    public string KdGol { get; set; }
    public string Nama { get; set; }
    public string Alamat { get; set; }
    public string Jabatan { get; set; }
    public string PDDK { get; set; }
    public string NPWP { get; set; }    
    public string StAktif { get; set; }
    public DateTime? DateCreate { get; set; }    
  }
}