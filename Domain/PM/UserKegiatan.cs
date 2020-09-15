using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Auth;
using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes.Joins;

namespace Domain.PM
{
  [Table("USERKEGIATAN")]
  public class UserKegiatan
  {
    [Key]
    public string UserId { get; set; }

    [InnerJoin("WEBUSER", "USERID", "USERID")]
    public WebUser WebUser { get; set; }

    [Key]
    public long IdKeg { get; set; }

    [InnerJoin("MKEGIATAN", "IDKEG", "IDKEG")]
    public MKegiatan MKegiatan { get; set; }

    public string AssignBy { get; set; }
    public DateTime? AssignDate { get; set; }
  }
}