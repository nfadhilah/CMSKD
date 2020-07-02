using System;

namespace Application.DM.BKBKasCQ
{
  public class BKBKasDTO
  {
    public string NoBBantu { get; set; }
    public long IdUnit { get; set; }
    public string KdUnit { get; set; }
    public string NmUnit { get; set; }
    public long IdRek { get; set; }
    public string KdPer { get; set; }
    public string NmPer { get; set; }
    public long IdBank { get; set; }
    public string NmBKas { get; set; }
    public string NoRek { get; set; }
    public Decimal? Saldo { get; set; }
  }
}
