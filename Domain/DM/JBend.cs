using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DM
{
  [Table("JBEND")]
  public class JBend
  {
    [Key]
    public string JnsBend { get; set; }
    public long IdRek { get; set; }
    public string UraiBend { get; set; }
  }
}
