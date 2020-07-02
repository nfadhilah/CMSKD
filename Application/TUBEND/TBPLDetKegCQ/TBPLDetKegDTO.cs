using System;

namespace Application.TUBEND.TBPLDetKegCQ
{
  public class TBPLDetKegDTO
  {
    public long IdTBPLDetKeg { get; set; }
    public long IdTBPLDet { get; set; }
    public int IdNoJeTra { get; set; }
    public string NmJeTra { get; set; }
    public string KdPersJeTra { get; set; }
    public long IdKeg { get; set; }
    public string NuKeg { get; set; }
    public string NmKegUnit { get; set; }
    public decimal? Nilai { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
