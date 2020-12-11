using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.TU
{
  public class BKUD
  {
    [Key]
    public string NoBuKas { get; set; }

    public string NoBBantu { get; set; }
    public string UnitKey { get; set; }
    public string NoSTS { get; set; }
    public string IdxTtd { get; set; }
    public string KdBukti { get; set; }
    public DateTime? TglKas { get; set; }
    public DateTime? TglValid { get; set; }
    public string Uraian { get; set; }
    public string NoBuktiKas { get; set; }
  }
}