using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroOrm.Dapper.Repositories.Attributes;

namespace Domain.DM
{
  [Table("JBM")]
  public class JBM
  {
    [Key, Identity]
    public long IdJBM { get; set; }
    [Key]
    public string KdBM { get; set; }
    public string NmBM { get; set; }
  }
}
