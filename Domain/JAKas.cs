using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroOrm.Dapper.Repositories.Attributes;

namespace Domain
{
  [Table("JAKAS")]
  public class JAKas
  {
    [Key, Identity]
    public long IdKas { get; set; }
    public string KdAKas { get; set; }
    public string NmAKas { get; set; }
    public string LabelKas { get; set; }
  }
}
