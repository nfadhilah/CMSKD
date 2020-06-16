using System.ComponentModel.DataAnnotations;
using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes.Joins;

namespace Domain.Auth
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
