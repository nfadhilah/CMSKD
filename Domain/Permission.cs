using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
  public class Permission
  {
    [Key]
    public int RoleId { get; set; }
    [InnerJoin("ROLES", "RoleId", "Id")]
    public Roles Role { get; set; }
    [Key]
    public int MenuId { get; set; }
    [InnerJoin("MENU", "MenuId", "Id")]
    public Menu Menu { get; set; }
    public bool Maker { get; set; }
    public bool Checker { get; set; }
    public bool Approver { get; set; }
  }
}
