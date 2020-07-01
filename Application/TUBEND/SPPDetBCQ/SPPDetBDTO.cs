using System;

namespace Application.TUBEND.SPPDetBCQ
{
  public class SPPDetBDTO
  {
    public long IdSPPDetB { get; set; }
    public long IdRek { get; set; }
    public string KdPer { get; set; }
    public string NmPer { get; set; }
    public long IdSPP { get; set; }
    public int IdNoJeTra { get; set; }
    public decimal? Nilai { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime DateUpdate { get; set; }
  }
}
