using System;

namespace Application.TUBEND.SPPDetRCQ
{
  public class SPPDetRDTO
  {
    public long IdSPPDetR { get; set; }
    public long IdRek { get; set; }
    public string KdPer { get; set; }
    public string NmPer { get; set; }
    public long IdKeg { get; set; }
    public string NuKeg { get; set; }
    public string NmKegUnit { get; set; }
    public long IdSPP { get; set; }
    public string NoSPP { get; set; }
    public int IdNoJeTra { get; set; }
    public decimal? Nilai { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime DateUpdate { get; set; }
  }
}
