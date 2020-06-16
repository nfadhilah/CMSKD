using MicroOrm.Dapper.Repositories.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Auth
{
  [Table("WEBROLE")]
  public class WebRole
  {
    [Key, Identity]
    public long RoleId { get; set; }
    public long IdApp { get; set; }
    public long IdParent { get; set; }
    public string Role { get; set; }
    public string Type { get; set; }
    public string MenuId { get; set; }
    public string Bantuan { get; set; }
  }
}
