using System;

namespace Application.TUBEND.STSDetBCQ
{
  public class STSDetBDTO
  {
    public long IdSTSDetB { get; set; }
    public long IdSTS { get; set; }
    public long IdRek { get; set; }
    public string KdPer { get; set; }
    public string NmPer { get; set; }
    public int IdNoJeTra { get; set; }
    public decimal? Nilai { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
