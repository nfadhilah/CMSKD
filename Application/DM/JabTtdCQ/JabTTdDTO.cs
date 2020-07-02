using System;

namespace Application.DM.JabTtdCQ
{
  public class JabTTdDTO
  {
    public long IdTtd { get; set; }
    public long IdUnit { get; set; }
    public string KdUnit { get; set; }
    public string NmUnit { get; set; }
    public long IdPeg { get; set; }
    public string NIP { get; set; }
    public string Nama { get; set; }
    public string KdGol { get; set; }
    public string KdDok { get; set; }
    public string Jabatan { get; set; }
    public string NoSKPTtd { get; set; }
    public DateTime? TglSKPTtd { get; set; }
    public string NoSKStopTtd { get; set; }
    public DateTime? TglSKStopTtd { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
