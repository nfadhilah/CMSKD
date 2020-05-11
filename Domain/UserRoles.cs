using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
  [Table("USERROLES")]
  public class UserRoles
  {
    [Key]
    public string UserId { get; set; }

    [InnerJoin("WEBUSER", "UserId", "UserId")]
    public WebUser WebUser { get; set; }

    [Key]
    public int RoleId { get; set; }

    [InnerJoin("ROLES", "RoleId", "Id")]
    public Roles Roles { get; set; }
  }
}