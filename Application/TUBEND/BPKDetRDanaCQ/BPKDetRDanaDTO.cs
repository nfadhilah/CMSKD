using System;

namespace Application.TUBEND.BPKDetRDanaCQ
{
  public class BPKDetRDanaDTO
  {
    public long IdBPKDetRDana { get; set; }
    public long IdBPKDetR { get; set; }
    public long IdJDana { get; set; }
    public string NmDana { get; set; }
    public string KetDana { get; set; }
    public decimal? Nilai { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
