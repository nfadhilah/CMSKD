using System;

namespace Application.DM.PajakCQ
{
  public class PajakDTO
  {
    public long IdPajak { get; set; }
    public string KdPajak { get; set; }
    public string NmPajak { get; set; }
    public string Uraian { get; set; }
    public string Keterangan { get; set; }
    public string RumusPajak { get; set; }
    public int? IdJnsPajak { get; set; }
    public int? StAktif { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
