using System;

namespace Application.TUBEND.TagihanDetCQ
{
  public class TagihanDetDTO
  {
    public long IdTagihanDet { get; set; }
    public long IdTagihan { get; set; }
    public long IdRek { get; set; }
    public string KdPer { get; set; }
    public string NmPer { get; set; }
    public decimal? Nilai { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
