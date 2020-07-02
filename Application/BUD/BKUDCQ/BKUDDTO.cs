using System;

namespace Application.BUD.BKUDCQ
{
  public class BKUDDTO
  {
    public long IdBKUD { get; set; }
    public long? IdKas { get; set; }
    public long IdSTS { get; set; }
    public string NoSTS { get; set; }
    public DateTime? TglSTS { get; set; }
    public string UraianSTS { get; set; }
    public long? IdBKas { get; set; }
    public long? IdUnit { get; set; }
    public string KdUnit { get; set; }
    public string NmUnit { get; set; }
    public DateTime? TglKas { get; set; }
    public DateTime? TglValid { get; set; }
    public string Uraian { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
