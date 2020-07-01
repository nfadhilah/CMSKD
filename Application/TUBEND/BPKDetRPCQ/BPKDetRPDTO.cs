using System;

namespace Application.TUBEND.BPKDetRPCQ
{
  public class BPKDetRPDTO
  {
    public long IdBPKDetRP { get; set; }
    public long IdBPKDetR { get; set; }
    public long IdPajak { get; set; }
    public string KdPajak { get; set; }
    public string NmPajak { get; set; }
    public string UraianPajak { get; set; }
    public decimal? Nilai { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
