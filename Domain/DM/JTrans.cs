using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DM
{
  [Table("JTRANS")]
  public class JTrans
  {
    [Key]
    public string IdTrans { get; set; }
    public string NmTrans { get; set; }
  }
}
