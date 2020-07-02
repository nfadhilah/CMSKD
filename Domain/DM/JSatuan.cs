using MicroOrm.Dapper.Repositories.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DM
{
  [Table("JSATUAN")]
  public class JSatuan
  {
    [Key, Identity]
    public long IdSatuan { get; set; }
    public string KdSatuan { get; set; }
    public string UraiSatuan { get; set; }
    public string Ket { get; set; }
  }
}
