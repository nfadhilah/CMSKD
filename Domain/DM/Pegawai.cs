using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DM
{
  [Table("PEGAWAI")]
  public class Pegawai
  {
    [Key] public string NIP { get; set; }
    public string KdGol { get; set; }
    public string UnitKey { get; set; }
    public string Nama { get; set; }
    public string Alamat { get; set; }
    public string Jabatan { get; set; }
    public string Pddk { get; set; }
  }
}