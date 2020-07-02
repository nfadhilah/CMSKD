using MicroOrm.Dapper.Repositories.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DM
{
  [Table("JBM")]
  public class JBM
  {
    [Key, Identity]
    public long IdJBM { get; set; }
    public string KdBM { get; set; }
    public string NmBM { get; set; }
  }
}
