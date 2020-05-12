using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
  [Table("USERROLES")]
  public class UserRoles
  {
    [Key, Identity]
    public int Id { get; set; }
    public int AppUserId { get; set; }

    [LeftJoin("APPUSER", "AppUserId", "Id")]
    public AppUser AppUser { get; set; }

    public int RoleId { get; set; }

    [LeftJoin("ROLES", "RoleId", "Id")]
    public Roles Roles { get; set; }
  }
}