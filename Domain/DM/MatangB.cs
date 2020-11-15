using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DM
{
  [Table("MATANGB")]
  public class MatangB
  {
    [Key]
    public string MtgKey { get; set; }
    public string KdPer { get; set; }
    public string NmPer { get; set; }
    public string MtgLevel { get; set; }
    public string KdKhusus { get; set; }
    public string Type { get; set; }
  }
}
