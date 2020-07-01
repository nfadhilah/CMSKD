using System;

namespace Application.TUBEND.BeritaCQ
{
  public class BeritaDTO
  {
    public long IdBerita { get; set; }
    public long IdUnit { get; set; }
    public string KdUnit { get; set; }
    public string NmUnit { get; set; }
    public long IdKeg { get; set; }
    public string NuKeg { get; set; }
    public string NmKegUnit { get; set; }
    public string NoBerita { get; set; }
    public DateTime TglBA { get; set; }
    public long IdKontrak { get; set; }
    public string NoKontrak { get; set; }
    public string Urai_Berita { get; set; }
    public DateTime? TglValid { get; set; }
    public string KdStatus { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
