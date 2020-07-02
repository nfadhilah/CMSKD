using System;

namespace Application.TUBEND.STSCQ
{
  public class STSDTO
  {
    public long IdSTS { get; set; }
    public long IdUnit { get; set; }
    public string NoSTS { get; set; }
    public long IdBend { get; set; }
    public string KdStatus { get; set; }
    public int IdxKode { get; set; }
    public long IdKas { get; set; }
    public DateTime? TglSTS { get; set; }
    public string Uraian { get; set; }
    public DateTime? TglValid { get; set; }
    public long? KdRilis { get; set; }
    public int? StKirim { get; set; }
    public int? StCair { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
