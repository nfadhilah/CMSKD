using System;

namespace Application.TUBEND.BkBankCQ
{
  public class BKBankDTO
  {
    public long IdBkBank { get; set; }
    public long IdUnit { get; set; }
    public string KdUnit { get; set; }
    public string NmUnit { get; set; }
    public long IdBend { get; set; }
    public string NoBuku { get; set; }
    public string KdStatus { get; set; }
    public DateTime? TglBuku { get; set; }
    public string Uraian { get; set; }
    public DateTime? TglValid { get; set; }
  }
}
