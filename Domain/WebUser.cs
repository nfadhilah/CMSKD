using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
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
    public string Nip { get; set; }
    public string GroupId { get; set; }
    [LeftJoin("WEBGROUP", "GROUPID", "GROUPID")]
    public WebGroup WebGroup { get; set; }
    public string Pwd { get; set; }
    public string Nama { get; set; }
    public int? BlokId { get; set; }
    public int? Transecure { get; set; }
    public int? StInsert { get; set; }
    public int? StUpdate { get; set; }
    public int? StDelete { get; set; }
    public string Ket { get; set; }
  }
}
