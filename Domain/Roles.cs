using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
  [Table("ROLES")]
  public class Roles
  {
    [Key, Identity]
    public int Id { get; set; }
    public string Name { get; set; }
    public string NormalizeName { get; set; }
    public string Description { get; set; }
    [LeftJoin("USERROLES", "Id", "RoleId")]
    public List<UserRoles> UserRoles { get; set; }
  }
}
