using MicroOrm.Dapper.Repositories.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DM
{
  [Table("DAFTBANK")]
  public class DaftBank
  {
    [Key, Identity]
    public long IdBank { get; set; }
    public string KdBank { get; set; }
    public string AkBank { get; set; }
    public string Alamat { get; set; }
    public string Telepon { get; set; }
    public string Cabang { get; set; }
    public DateTime? DateCreate { get; set; }
  }
}
