using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroOrm.Dapper.Repositories.Attributes;

namespace Domain
{
  [Table("JBEND")]
  public class JBend
  {
    [Key, Identity]
    public long IdJBend { get; set; }
    [Key]
    public string Jns_Bend { get; set; }
    public long IdRek { get; set; }
    public string Urai_Bend { get; set; }
  }
}
