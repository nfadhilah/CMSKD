using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroOrm.Dapper.Repositories.Attributes;

namespace Domain
{
  [Table("JTRANS")]
  public class JTrans
  {
    [Key, Identity]
    public long IdJTrans { get; set; }
    [Key]
    public string IdTrans { get; set; }
    public string NmTrans { get; set; }
  }
}
