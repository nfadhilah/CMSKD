using System;

namespace Application.BUD.DPDetCQ
{
  public class DPDetDTO
  {
    public long IdDPDet { get; set; }
    public long IdDP { get; set; }
    public long IdSP2D { get; set; }
    public string NoSP2D { get; set; }
    public DateTime? TglSP2D { get; set; }
    public string UraianSP2D { get; set; }
    public DateTime? DateCreate { get; set; }
  }
}
