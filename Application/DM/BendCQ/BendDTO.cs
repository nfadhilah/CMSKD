using System;

namespace Application.DM.BendCQ
{
  public class BendDTO
  {
    public long IdBend { get; set; }
    public string JnsBend { get; set; }
    public long IdPeg { get; set; }
    public string NIP { get; set; }
    public string Nama { get; set; }
    public string IdBank { get; set; }
    public string KdBank { get; set; }
    public string NmBank { get; set; }
    public string NmCabBank { get; set; }
    public string RekBend { get; set; }
    public string NPWPBend { get; set; }
    public string JabBend { get; set; }
    public Decimal? SaldoBend { get; set; }
    public Decimal? SaldoBendT { get; set; }
    public DateTime? TglStopBend { get; set; }
    public string WargaNegara { get; set; }
    public int? StAktif { get; set; }
    public DateTime? DateCreate { get; set; }
  }
}
