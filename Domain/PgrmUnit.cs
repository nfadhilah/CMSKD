using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
  [Table("PGRMUNIT")]
  public class PgrmUnit
  {
    [Key]
    public int KdTahap { get; set; }

    [Key]
    public string IdUnit { get; set; }

    [Key]
    public string IdPrgrm { get; set; }

    public string Target { get; set; }
    public string Sasaran { get; set; }
    public string NoPrio { get; set; }
  }
}