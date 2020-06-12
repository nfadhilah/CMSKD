using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroOrm.Dapper.Repositories.Attributes;

namespace Domain
{
  [Table("FUNGSI")]
  public class DaftFungsi
  {
    [Key, Identity]
    public long IdFung { get; set; }
    public string KdFung { get; set; }
    public string NmFung { get; set; }
  }
}
