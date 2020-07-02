using MicroOrm.Dapper.Repositories.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DM
{
  [Table("STRUUNIT")]
  public class StruUnit
  {
    [Key, Identity]
    public long IdStruUnit { get; set; }
    public int KdLevel { get; set; }
    public string NmLevel { get; set; }
    public string NumDigit { get; set; }
  }
}
