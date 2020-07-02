using System;

namespace Application.BUD.SP2DDetBDanaCQ
{
  public class SP2DDetBDanaDTO
  {
    public long IdSP2DDetBDana { get; set; }
    public long IdSP2DDetB { get; set; }
    public long IdJDana { get; set; }
    public string NmDana { get; set; }
    public string KetDana { get; set; }
    public decimal? Nilai { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
