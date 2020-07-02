using System;

namespace Application.TUBEND.TBPLDetCQ
{
  public class TBPLDetDTO
  {
    public long IdTBPLDet { get; set; }
    public long IdTBPL { get; set; }
    public long IdBend { get; set; }
    public int IdNoJetTra { get; set; }
    public string NmJeTra { get; set; }
    public string KdPersJeTra { get; set; }
    public decimal? Nilai { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
