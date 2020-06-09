using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroOrm.Dapper.Repositories.Attributes;

namespace Domain
{
  [Table("DAFTBANK")]
  public class DaftBank
  {
    [Key, Identity]
    public long IdBank { get; set; }
    [Key]
    public string KdBank { get; set; }
    public string AkBank { get; set; }
    public string Alamat { get; set; }
    public string Telepon { get; set; }
    public string Cabang { get; set; }
    public DateTime? DateCreate { get; set; }
  }
}
