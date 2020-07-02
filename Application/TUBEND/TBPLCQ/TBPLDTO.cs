using System;

namespace Application.TUBEND.TBPLCQ
{
  public class TBPLDTO
  {
    public long IdTBPL { get; set; }
    public long IdUnit { get; set; }
    public string KdUnit { get; set; }
    public string NmUnit { get; set; }
    public string NoTBPL { get; set; }
    public long IdBend { get; set; }
    public string KdStatus { get; set; }
    public int IdxKode { get; set; }
    public DateTime? TglTBPL { get; set; }
    public string Penyetor { get; set; }
    public string Alamat { get; set; }
    public string UraiTBPL { get; set; }
    public DateTime? TglValid { get; set; }
    public long? KdRilis { get; set; }
    public int? StKirim { get; set; }
    public int? StCair { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
