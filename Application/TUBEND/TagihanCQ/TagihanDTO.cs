using System;

namespace Application.TUBEND.TagihanCQ
{
  public class TagihanDTO
  {
    public long IdTagihan { get; set; }
    public long IdUnit { get; set; }
    public long IdKeg { get; set; }
    public string NoTagihan { get; set; }
    public DateTime TglTagihan { get; set; }
    public long IdKontrak { get; set; }
    public string NoKontrak { get; set; }
    public string UraianTagihan { get; set; }
    public DateTime? TglValid { get; set; }
    public string KdStatus { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
