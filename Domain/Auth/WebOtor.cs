using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroOrm.Dapper.Repositories.Attributes.Joins;

namespace Domain.Auth
{
  [Table("WEBOTOR")]
  public class WebOtor
  {
    [Key] public string GroupId { get; set; }

    [InnerJoin("WEBGROUP", "GROUPID", "GROUPID")]
    public WebGroup WebGroup { get; set; }

    [Key] public string RoleId { get; set; }

    [InnerJoin("WEBROLE", "ROLEID", "ROLEID")]
    public WebRole WebRole { get; set; }
  }
}