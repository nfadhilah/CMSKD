using System;

namespace Application.BUD.SP2DCQ
{
  public class SP2DDTO
  {
    public long IdSP2D { get; set; }
    public string NoSP2D { get; set; }
    public long IdUnit { get; set; }
    public string KdUnit { get; set; }
    public string NmUnit { get; set; }
    public string KdStatus { get; set; }
    public long IdSPM { get; set; }
    public string NoSPM { get; set; }
    public long? IdBend { get; set; }
    public long? IdSPD { get; set; }
    public string NoSPD { get; set; }
    public DateTime? TglSPD { get; set; }
    public string KeteranganSPD { get; set; }
    public long? IdPhk3 { get; set; }
    public long? IdTtd { get; set; }
    public int? IdxKode { get; set; }
    public string NoReg { get; set; }
    public string KetOtor { get; set; }
    public long? IdKontrak { get; set; }
    public string NoKontrak { get; set; }
    public string Keperluan { get; set; }
    public string Penolakan { get; set; }
    public DateTime? TglValid { get; set; }
    public DateTime? TglSP2D { get; set; }
    public DateTime? TglSPM { get; set; }
    public string NoBBantu { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
