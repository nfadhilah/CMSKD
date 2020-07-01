using System;

namespace Application.TUBEND.BPKCQ
{
  public class BPKDTO
  {
    public long IdBPK { get; set; }
    public long IdUnit { get; set; }
    public string KdUnit { get; set; }
    public string NmUnit { get; set; }
    public long IdPhk3 { get; set; }
    public string NmPhk3 { get; set; }
    public string NPWP { get; set; }
    public string NoBPK { get; set; }
    public string KdStatus { get; set; }
    public long IdJBayar { get; set; }
    public string UraianBayar { get; set; }
    public int IdxKode { get; set; }
    public long IdBend { get; set; }
    public DateTime? TglBPK { get; set; }
    public string UraiBPK { get; set; }
    public DateTime? TglValid { get; set; }
    public long? IdBerita { get; set; }
    public string NoBerita { get; set; }
    public string TglBA { get; set; }
    public long? KdRilis { get; set; }
    public int? StKirim { get; set; }
    public int? StCair { get; set; }
    public string NoRef { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
