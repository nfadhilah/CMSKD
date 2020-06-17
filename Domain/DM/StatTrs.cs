using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DM
{
  [Table("STATTRS")]
  public class StatTrs
  {
    [Key]
    public string KdStatus { get; set; }
    public string LblStatus { get; set; }
    public string Uraian { get; set; }
  }
}
