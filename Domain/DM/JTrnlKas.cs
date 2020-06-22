using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DM
{
  [Table("JTRNLKAS")]
  public class JTrnlKas
  {
    [Key]
    public int IdNoJeTra { get; set; }
    public string NmJeTra { get; set; }
    public string KdPers { get; set; }
  }
}
