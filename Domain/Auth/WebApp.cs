using MicroOrm.Dapper.Repositories.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Auth
{
  [Table("WEBAPP")]
  public class WebApp
  {
    [Key, Identity]
    public long IdApp { get; set; }
    public string ProdukId { get; set; }
    public string NmApp { get; set; }
    public string SerialKey { get; set; }
  }
}
