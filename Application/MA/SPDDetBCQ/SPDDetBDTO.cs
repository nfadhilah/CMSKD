using System;

namespace Application.MA.SPDDetBCQ
{
  public class SPDDetBDTO
  {
    public long IdSPDDetB { get; set; }
    public long IdSPD { get; set; }
    public string NoSPD { get; set; }
    public long IdRek { get; set; }
    public string KdPer { get; set; }
    public string NmPer { get; set; }
    public decimal? Nilai { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
