using System;

namespace Application.TUBEND.SPMCQ
{
  public class SPMDTO
  {
    public long IdSPM { get; set; }
    public long IdUnit { get; set; }
    public string KdUnit { get; set; }
    public string NmUnit { get; set; }
    public string NoSPM { get; set; }
    public DateTime? TglSPM { get; set; }
    public string KdStatus { get; set; }
    public long IdBend { get; set; }
    public long IdSPD { get; set; }
    public string NoSPD { get; set; }
    public DateTime? TglSPD { get; set; }
    public long IdSPP { get; set; }
    public string NoSPP { get; set; }
    public DateTime? TglSPP { get; set; }
    public long? IdPhk3 { get; set; }
    public string NmPhk3 { get; set; }
    public string NmInstPhk3 { get; set; }
    public int IdxKode { get; set; }
    public string NoReg { get; set; }
    public string KetOtor { get; set; }
    public long? IdKontrak { get; set; }
    public string NoKontrak { get; set; }
    public string Keperluan { get; set; }
    public string Penolakan { get; set; }
    public DateTime? TglValid { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
