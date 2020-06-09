using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroOrm.Dapper.Repositories.Attributes;

namespace Domain
{
  [Table("JBKAS")]
  public class JBKas
  {
    [Key, Identity]
    public long IdBKas { get; set; }
    public string KdBKas { get; set; }
    public string NmBKas { get; set; }
  }
}
