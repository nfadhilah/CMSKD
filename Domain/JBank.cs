using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroOrm.Dapper.Repositories.Attributes;

namespace Domain
{
  [Table("JBANK")]
  public class JBank
  {
    [Key, Identity]
    public long IdJBank { get; set; }
    public string KdBank { get; set; }
    public string NmBank { get; set; }
    public string Uraian { get; set; }
    public string Akronim { get; set; }
    public DateTime? DateCreate { get; set; }
  }
}
