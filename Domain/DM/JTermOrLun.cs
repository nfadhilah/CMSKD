using MicroOrm.Dapper.Repositories.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DM
{
  [Table("JTERMORLUN")]
  public class JTermOrLun
  {
    [Key, Identity]
    public long IdJTermOrLun { get; set; }
    public string NmJTermOrLun { get; set; }
  }
}
