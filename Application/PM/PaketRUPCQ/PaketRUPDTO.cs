using System;

namespace Application.PM.PaketRUPCQ
{
  public class PaketRUPDTO
  {
    public long IdRUP { get; set; }
    public long IdUnit { get; set; }
    public int JnsRUP { get; set; }
    public int TipeSwakelola { get; set; }
    public string UraiTipeSwakelola { get; set; }
    public string KdUnit { get; set; }
    public string NmUnit { get; set; }
    public long IdKeg { get; set; }
    public string NuSubKeg { get; set; }
    public string NmSubKeg { get; set; }
    public decimal? NilaiPagu { get; set; }
    public DateTime? TglValid { get; set; }
    public string KodeRUP { get; set; }
    public int IdLokasi { get; set; }
    public string NmPaket { get; set; }
    public string Lokasi { get; set; }
    public string Volume { get; set; }
    public string UraiPaket { get; set; }
    public int Status { get; set; }
    public string LblStatus { get; set; }
    public long? IdJnsPekerjaan { get; set; }
    public string UraianJnsPekerjaan { get; set; }
    public long? IdMetodePengadaan { get; set; }
    public string UraianMetodePengadaan { get; set; }
    public DateTime? AwalPekerjaan { get; set; }
    public DateTime? AkhirPekerjaan { get; set; }
    public long IdJDana { get; set; }
    public string KdDana { get; set; }
    public string NmDana { get; set; }
    public long IdPhk3 { get; set; }
    public string NmPhk3 { get; set; }
    public string NmInstPhk3 { get; set; }
    public string NPWPPhk3 { get; set; }
	public bool? A { get; set; }
    public bool? FD { get; set; }
    public bool? U { get; set; }
    public string CreatedBy { get; set; }
  }
}