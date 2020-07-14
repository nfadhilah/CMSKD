using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DM
{
  [Table("TAHAP")]
  public class Tahap
  {
    [Key]
    public string KdTahap { get; set; }
    public string Uraian { get; set; }
    public DateTime? TglTransfer { get; set; }
  }
}
