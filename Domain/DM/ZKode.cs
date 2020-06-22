using System.ComponentModel.DataAnnotations;

namespace Domain.DM
{
  public class ZKode
  {
    [Key]
    public int IdxKode { get; set; }
    public string Uraian { get; set; }
  }
}
