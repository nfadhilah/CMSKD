using MicroOrm.Dapper.Repositories.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DM
{
  [Table("STRUREK")]
  public class StruRek
  {
    [Key, Identity]
    public long IdStruRek { get; set; }
    public int MtgLevel { get; set; }
    public string NmLevel { get; set; }
  }
}
