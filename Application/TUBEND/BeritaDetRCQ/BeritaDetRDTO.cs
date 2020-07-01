using System;

namespace Application.TUBEND.BeritaDetRCQ
{
  public class BeritaDetRDTO
  {
    public long IdBeritaDet { get; set; }
    public long IdBerita { get; set; }
    public string NoBerita { get; set; }
    public long IdRek { get; set; }
    public string KdPer { get; set; }
    public string NmPer { get; set; }
    public decimal? Nilai { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
