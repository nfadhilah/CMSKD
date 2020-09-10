using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Auth
{
  [Table("WEBGROUP")]
  public class WebGroup
  {
    [Key]
    public long GroupId { get; set; }

    public string NmGroup { get; set; }
    public string Ket { get; set; }
  }
}