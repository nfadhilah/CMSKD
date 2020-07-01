using System;

namespace Application.TUBEND.SPPDetRDanaCQ
{
  public class SPPDetRDanaDTO
  {
    public long IdSPPDetRDana { get; set; }
    public long IdSPPDetR { get; set; }
    public long IdJDana { get; set; }
    public string KdDana { get; set; }
    public string NmDana { get; set; }
    public string KetDana { get; set; }
    public decimal? Nilai { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
