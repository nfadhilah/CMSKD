using System;

namespace Application.TUBEND.STSDetDCQ
{
  public class STSDetDDTO
  {
    public long IdSTSDetD { get; set; }
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
