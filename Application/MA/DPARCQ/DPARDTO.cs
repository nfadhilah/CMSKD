using System;

namespace Application.MA.DPARCQ
{
  public class DPARDTO
  {
    public long IdDPAR { get; set; }
    public long IdDPA { get; set; }
    public string NoDPA { get; set; }
    public string KdTahap { get; set; }
    public int? IdXKode { get; set; }
    public long IdKeg { get; set; }
    public string NuKeg { get; set; }
    public string NmKegUnit { get; set; }
    public long IdRek { get; set; }
    public string KdPer { get; set; }
    public string NmPer { get; set; }
    public Decimal? Nilai { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
