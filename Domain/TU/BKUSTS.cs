using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.TU
{
  [Table("BKUSTS")]
  public class BKUSTS
  {
    [Key]
    public string UnitKey { get; set; }
    [Key]
    public string NoBKUSKPD { get; set; }
    public string NoSTS { get; set; }
    public string IdxTtd { get; set; }
    public DateTime? TglBKUSKPD { get; set; }
    public string Uraian { get; set; }
    public DateTime? TglValid { get; set; }
    public string KeyBend { get; set; }
  }
}