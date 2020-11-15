using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DM
{
  [Table("JPAJAK")]
  public class JPajak
  {
    [Key]
    public string PjkKey { get; set; }
    public string KdPajak { get; set; }
    public string NmPajak { get; set; }
    public string RumusPjk { get; set; }
  }
}
