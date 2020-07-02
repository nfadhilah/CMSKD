using MicroOrm.Dapper.Repositories.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DM
{
  [Table("JNSAKUN")]
  public class JnsAkun
  {
    [Key, Identity]
    public long IdJnsAkun { get; set; }
    public string UraiAkun { get; set; }
    public string KdPers { get; set; }
  }
}
