using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DM
{
  [Table("WEBGROUP")]
  public class WebGroup
  {
    [Key]
    public string GroupId { get; set; }
    public string NmGroup { get; set; }
    public string Ket { get; set; }
  }
}