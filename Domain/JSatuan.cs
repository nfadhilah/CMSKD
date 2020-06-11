using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroOrm.Dapper.Repositories.Attributes;

namespace Domain
{
  [Table("JSATUAN")]
  public class JSatuan
  {
    [Key, Identity]
    public long IdSatuan { get; set; }
    [Key]
    public string KdSatuan { get; set; }
    public string UraiSatuan { get; set; }
    public string Ket { get; set; }
  }
}
