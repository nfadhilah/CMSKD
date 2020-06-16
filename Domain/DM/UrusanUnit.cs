using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;

namespace Domain.DM
{
  [Table("URUSANUNIT")]
  public class UrusanUnit
  {
    [Key, Identity]
    public long IdUrusanUnit { get; set; }
    public long IdUnit { get; set; }
    [LeftJoin("DAFTUNIT", "IDUNIT", "IDUNIT", TableAlias = "UNIT")]
    public DaftUnit DaftUnit { get; set; }
    public long UrusKey { get; set; }
    [LeftJoin("DAFTUNIT", "URUSKEY", "IDUNIT", TableAlias = "URUSAN")]
    public DaftUnit Urusan { get; set; }
  }
}
