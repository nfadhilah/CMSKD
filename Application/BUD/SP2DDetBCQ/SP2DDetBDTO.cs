using System;

namespace Application.BUD.SP2DDetBCQ
{
  public class SP2DDetBDTO
  {
    public long IdSP2DDetB { get; set; }
    public long IdSP2D { get; set; }
    public string NoSP2D { get; set; }
    public long IdRek { get; set; }
    public string KdPer { get; set; }
    public string NmPer { get; set; }
    public int IdNoJeTra { get; set; }
    public decimal? Nilai { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
