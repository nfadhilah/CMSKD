using System;

namespace Application.MA.DPABCQ
{
  public class DPABDTO
  {

    public long IdDPAB { get; set; }
    public long IdDPA { get; set; }
    public string NoDPA { get; set; }
    public int? IdXKode { get; set; }
    public string KdTahap { get; set; }
    public long IdRek { get; set; }
    public string KdPer { get; set; }
    public string NmPer { get; set; }
    public decimal? Nilai { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
