using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DM
{
  [Table("URUSANUNIT")]
  public class UrusanUnit
  {
    [Identity]
    public long IdUrusanUnit { get; set; }
    [Key]
    public long IdUnit { get; set; }
    [LeftJoin("DAFTUNIT", "IDUNIT", "IDUNIT", TableAlias = "UNIT")]
    public DaftUnit DaftUnit { get; set; }
    [Key]
    public long IdUrus { get; set; }
    [LeftJoin("DAFTUNIT", "IDURUS", "IDURUS", TableAlias = "URUSAN")]
    public DaftUnit Urusan { get; set; }
  }
}
