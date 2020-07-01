using System;

namespace Application.TUBEND.KontrakCQ
{
  public class KontrakDTO
  {
    public long IdKontrak { get; set; }
    public long IdUnit { get; set; }
    public string KdUnit { get; set; }
    public string NmUnit { get; set; }
    public string NoKontrak { get; set; }
    public long IdPhk3 { get; set; }
    public string NmPhk3 { get; set; }
    public string NmInstPhk3 { get; set; }
    public long IdKeg { get; set; }
    public string NuKeg { get; set; }
    public string NmKegUnit { get; set; }
    public DateTime? TglKon { get; set; }
    public DateTime? TglAwalKontrak { get; set; }
    public DateTime? TglAkhirKontrak { get; set; }
    public DateTime? TglSlsKontrak { get; set; }
    public string Uraian { get; set; }
    public decimal? Nilai { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
