using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Auth
{
  [Table("WEBOTOR")]
  public class WebOtor
  {
    [Key]
    public long GroupId { get; set; }
    [InnerJoin("WEBGROUP", "GROUPID", "GROUPID")]
    public WebGroup WebGroup { get; set; }
    [Key]
    public long RoleId { get; set; }
    [InnerJoin("WEBROLE", "ROLEID", "ROLEID")]
    public WebRole WebRole { get; set; }
  }
}