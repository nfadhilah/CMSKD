using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Auth
{
  [Table("WEBUSER")]
  public class WebUser
  {
    [Key]
    public string UserId { get; set; }
    public long? IdUnit { get; set; }
    [LeftJoin("DAFTUNIT", "IDUNIT", "IDUNIT")]
    public DaftUnit DaftUnit { get; set; }
    public int KdTahap { get; set; }
    public long? IdPeg { get; set; }
    public string Pwd { get; set; }
    public long GroupId { get; set; }
    [LeftJoin("WEBGROUP", "GROUPID", "GROUPID")]
    public WebGroup WebGroup { get; set; }
    public string Nama { get; set; }
    public string Email { get; set; }
    public int? BlokId { get; set; }
    public bool Transecure { get; set; }
    public bool StInsert { get; set; }
    public bool StUpdate { get; set; }
    public bool StDelete { get; set; }
    public string Ket { get; set; }
  }
}