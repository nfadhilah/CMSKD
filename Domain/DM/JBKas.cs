using MicroOrm.Dapper.Repositories.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DM
{
  [Table("JBKAS")]
  public class JBKas
  {
    [Key, Identity]
    public long IdBKas { get; set; }
    public string NmBKas { get; set; }
  }
}
