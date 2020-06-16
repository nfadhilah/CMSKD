using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Auth
{
  [Table("WEBOTOR")]
  public class WebOtor
  {
    [Key]
    public long GroupId { get; set; }
    [Key]
    public long RoleId { get; set; }
  }
}