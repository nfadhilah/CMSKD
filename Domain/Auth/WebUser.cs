using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes.Joins;

namespace Domain.Auth
{
  [Table("WEBUSER")]
    public class WebUser
    {
      [Key]
      public string UserId { get; set; }
      public int KdTahap { get; set; }
      public string UnitKey { get; set; }
      [LeftJoin("DAFTUNIT", "UNITKEY", "UNITKEY")]
      public DaftUnit DaftUnit { get; set; }
      public string NIP { get; set; }
      public string GroupId { get; set; }
      [InnerJoin("WEBGROUP", "GROUPID", "GROUPID")]
      public WebGroup WebGroup { get; set; }
      public string Pwd { get; set; }
      public string Nama { get; set; }
      public int? BlokId { get; set; }
      public int? TranSecure { get; set; }
      public int? StInsert { get; set; }
      public int? StUpdate { get; set; }
      public int? StDelete { get; set; }
      public string Ket { get; set; }
    }
}
