using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroOrm.Dapper.Repositories.Attributes;

namespace Domain.DM
{
  [Table("GOLONGAN")]
  public class Golongan
  {
    [Key, Identity]
    public long IdGol { get; set; }
    [Key]
    public string KdGol { get; set; }
    public string NmGol { get; set; }
    public string Pangkat { get; set; }
  }
}
