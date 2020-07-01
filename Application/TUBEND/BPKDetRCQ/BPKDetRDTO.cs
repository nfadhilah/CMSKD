using System;

namespace Application.TUBEND.BPKDetRCQ
{
  public class BPKDetRDTO
  {
    public long IdBPKDetR { get; set; }
    public long IdBPK { get; set; }
    public string NoBPK { get; set; }
    public long IdKeg { get; set; }
    public string NuKeg { get; set; }
    public string NmKegUnit { get; set; }
    public long IdRek { get; set; }
    public string KdPer { get; set; }
    public string NmPer { get; set; }
    public int IdNoJeTra { get; set; }
    public decimal? Nilai { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
