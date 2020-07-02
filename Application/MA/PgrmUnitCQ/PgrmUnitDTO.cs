using System;

namespace Application.MA.PgrmUnitCQ
{
  public class PgrmUnitDTO
  {
    public long IdPgrmUnit { get; set; }
    public long IdUnit { get; set; }
    public string KdUnit { get; set; }
    public string NmUnit { get; set; }
    public string KdTahap { get; set; }
    public long IdPrgrm { get; set; }
    public string NuPrgrm { get; set; }
    public string NmPrgrm { get; set; }
    public string Target { get; set; }
    public string Sasaran { get; set; }
    public int? NoPrio { get; set; }
    public string Indikator { get; set; }
    public string Ket { get; set; }
    public string IdSas { get; set; }
    public DateTime? TglValid { get; set; }
    public int? IdXKode { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
