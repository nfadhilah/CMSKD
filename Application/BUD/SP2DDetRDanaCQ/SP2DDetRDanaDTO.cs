using System;

namespace Application.BUD.SP2DDetRDanaCQ
{
  public class SP2DDetRDanaDTO
  {
    public long IdSP2DDetRDana { get; set; }
    public long IdSP2DDetR { get; set; }
    public long IdJDana { get; set; }
    public string NmDana { get; set; }
    public string KetDana { get; set; }
    public decimal? Nilai { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
