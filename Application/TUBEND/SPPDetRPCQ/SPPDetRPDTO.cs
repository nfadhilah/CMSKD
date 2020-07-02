using System;

namespace Application.TUBEND.SPPDetRPCQ
{
  public class SPPDetRPDTO
  {
    public long IdSPPDetRP { get; set; }
    public long IdSPPDetR { get; set; }
    public long IdPajak { get; set; }
    public string KdPajak { get; set; }
    public string NmPajak { get; set; }
    public string UraianPajak { get; set; }
    public decimal? Nilai { get; set; }
    public string Keterangan { get; set; }
    public string IdBilling { get; set; }
    public DateTime? TglBilling { get; set; }
    public string NTPN { get; set; }
    public string NTB { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
