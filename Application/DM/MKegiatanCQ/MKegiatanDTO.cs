using System;

namespace Application.DM.MKegiatanCQ
{
  public class MKegiatanDTO
  {
    public long IdKeg { get; set; }
    public long IdPrgrm { get; set; }
    public string NuPrgrm { get; set; }
    public string NmPrgrm { get; set; }
    public string KdPerspektif { get; set; }
    public string NuKeg { get; set; }
    public string NmKegUnit { get; set; }
    public int LevelKeg { get; set; }
    public string Type { get; set; }
    public long? IdKegInduk { get; set; }
    public bool? StAktif { get; set; }
    public bool? StValid { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
