using System;

namespace Application.PM.PaketRUPDetCQ
{
  public class PaketRUPDetDTO
  {
    public long? IdRUPDet { get; set; }
    public long? IdRUP { get; set; }
    public string KodeRUP { get; set; }
    public string NmPaket { get; set; }
    public string Lokasi { get; set; }
    public string Volume { get; set; }
    public string UraiPaket { get; set; }
    public long? IdJnsPekerjaan { get; set; }
    public string JnsPekerjaan { get; set; }
    public DateTime? AwalPekerjaan { get; set; }
    public DateTime? AkhirPekerjaan { get; set; }
    public long? IdJDana { get; set; }
    public string KdDana { get; set; }
    public string NmDana { get; set; }
    public long? IdPhk3 { get; set; }
    public string NmPhk3 { get; set; }
    public string NmInst { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}