using System;

namespace Application.TUBEND.BKPajakCQ
{
  public class BkPajakDTO
  {
    public long IdBkPajak { get; set; }
    public long IdUnit { get; set; }
    public string KdUnit { get; set; }
    public string NmUnit { get; set; }
    public long IdBend { get; set; }
    public string NoBkPajak { get; set; }
    public int IdxKode { get; set; }
    public string KdStatus { get; set; }
    public DateTime? TglBkPajak { get; set; }
    public string Uraian { get; set; }
    public DateTime? TglValid { get; set; }
    public long? KdRilis { get; set; }
    public int? StKirim { get; set; }
    public int? StCair { get; set; }
    public int IdTransfer { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}