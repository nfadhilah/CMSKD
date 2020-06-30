using MicroOrm.Dapper.Repositories.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DM
{
  [Table("JUSAHA")]
  public class JUsaha
  {
    [Key, Identity]
    public long IdJUsaha { get; set; }
    public string BadanUsaha { get; set; }
    public string Keterangan { get; set; }
    public string Akronim { get; set; }
  }
}
