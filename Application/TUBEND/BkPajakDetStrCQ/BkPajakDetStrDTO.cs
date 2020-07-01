using System;

namespace Application.TUBEND.BkPajakDetStrCQ
{
  public class BkPajakDetStrDTO
  {
    public long IdBkPajakDetStr { get; set; }
    public long IdBkPajakStr { get; set; }
    public long? IdPajak { get; set; }
    public string KdPajak { get; set; }
    public string NmPajak { get; set; }
    public long IdBkPajak { get; set; }
    public string NoBkPajak { get; set; }
    public string IdBilling { get; set; }
    public DateTime? TglIdBilling { get; set; }
    public DateTime? TglExpire { get; set; }
    public string NTPN { get; set; }
    public string NTB { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
