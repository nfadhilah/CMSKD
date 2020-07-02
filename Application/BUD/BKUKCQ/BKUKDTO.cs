using System;

namespace Application.BUD.BKUKCQ
{
  public class BKUKDTO
  {
    public long IdBKUK { get; set; }
    public long? IdKas { get; set; }
    public long IdSP2D { get; set; }
    public string NoSP2D { get; set; }
    public DateTime? TglSP2D { get; set; }
    public string UraianSP2D { get; set; }
    public long? IdBKas { get; set; }
    public long? IdUnit { get; set; }
    public string KdUnit { get; set; }
    public string NmUnit { get; set; }
    public DateTime? TglKas { get; set; }
    public string Uraian { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
