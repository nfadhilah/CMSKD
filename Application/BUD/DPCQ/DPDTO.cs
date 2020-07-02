using System;

namespace Application.BUD.DPCQ
{
  public class DPDTO
  {
    public long IdDP { get; set; }
    public string NoDP { get; set; }
    public int? IdxKode { get; set; }
    public long? IdTtd { get; set; }
    public DateTime? TglDP { get; set; }
    public string Uraian { get; set; }
    public DateTime? TglValid { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
