using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Auth
{
  [Table("WEBROLE")]
  public class WebRole
  {
    [Key]
    public string RoleId { get; set; }
    public string Role { get; set; }
    public string Type { get; set; }
    public long MenuId { get; set; }
    public string RouterLink { get; set; }
    public string Icon { get; set; }
    public string Bantuan { get; set; }
    public int? KdLevel { get; set; }
    public int? AppId { get; set; }
  }
}