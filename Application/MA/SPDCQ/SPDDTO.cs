using System;

namespace Application.MA.SPDCQ
{
  public class SPDDTO
  {
    public long IdSPD { get; set; }
    public long IdUnit { get; set; }
    public string KdUnit { get; set; }
    public string NmUnit { get; set; }
    public string NoSPD { get; set; }
    public DateTime? TglSPD { get; set; }
    public int KdBulan1 { get; set; }
    public int KdBulan2 { get; set; }
    public int IdxKode { get; set; }
    public string Keterangan { get; set; }
    public DateTime? TglValid { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
