using System;

namespace Application.TUBEND.SPPDetBDanaCQ
{
  public class SPPDetBDanaDTO
  {
    public long IdSPPDetBDana { get; set; }
    public long IdSPPDetB { get; set; }
    public long IdJDana { get; set; }
    public string KdDana { get; set; }
    public string NmDana { get; set; }
    public string KetDana { get; set; }
    public decimal? Nilai { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
