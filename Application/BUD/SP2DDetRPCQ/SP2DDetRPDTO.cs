using System;

namespace Application.BUD.SP2DDetRPCQ
{
  public class SP2DDetRPDTO
  {
    public long IdSP2DDetRP { get; set; }
    public long IdSP2DDetR { get; set; }
    public long IdPajak { get; set; }
    public string KdPajak { get; set; }
    public string NmPajak { get; set; }
    public string UraianPajak { get; set; }
    public decimal? Nilai { get; set; }
    public string Keterangan { get; set; }
    public string IdBilling { get; set; }
    public DateTime? TglBilling { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
