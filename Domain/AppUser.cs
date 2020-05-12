using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
  [Table("APPUSER")]
  public class AppUser
  {
    [Key, Identity]
    public int Id { get; set; }

    public string UserName { get; set; }
    public string DisplayName { get; set; }
    public int KdTahap { get; set; }
    public string UnitKey { get; set; }

    [LeftJoin("DAFTUNIT", "UnitKey", "UnitKey")]
    public DaftUnit DaftUnit { get; set; }

    public string NIP { get; set; }
    public string Password { get; set; }
    public int FalseLoginCount { get; set; }
    public bool LockedOut { get; set; }
    public string Description { get; set; }

    [LeftJoin("USERROLES", "Id", "AppUserId")]
    public List<UserRoles> UserRoles { get; set; }
  }
}