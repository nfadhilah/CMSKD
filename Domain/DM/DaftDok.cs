using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DM
{
  [Table("DAFTDOK")]
  public class DaftDok
  {
    [Key]
    public string KdDok { get; set; }
    public string NmDok { get; set; }
    public string Ket { get; set; }
  }
}
