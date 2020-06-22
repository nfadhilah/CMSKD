using MicroOrm.Dapper.Repositories.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DM
{
  [Table("JTAHUN")]
  public class JTahun
  {
    [Key, Identity]
    public int IdTahun { get; set; }
    public string Tahun { get; set; }
    public string NmTahun { get; set; }
  }
}
