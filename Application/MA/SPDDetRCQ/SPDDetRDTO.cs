using System;

namespace Application.MA.SPDDetRCQ
{
  public class SPDDetRDTO
  {
    public long IdSPDDetR { get; set; }
    public long IdSPD { get; set; }
    public string NoSPD { get; set; }
    public long IdKeg { get; set; }
    public string NuKeg { get; set; }
    public string NmKegUnit { get; set; }
    public long IdRek { get; set; }
    public string KdPer { get; set; }
    public string NmPer { get; set; }
    public decimal? Nilai { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
