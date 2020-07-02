using System;

namespace Application.DM.DaftBankCQ
{
  public class DaftBankDTO
  {
    public long IdBank { get; set; }
    public string KdBank { get; set; }
    public string AkBank { get; set; }
    public string Alamat { get; set; }
    public string Telepon { get; set; }
    public string Cabang { get; set; }
    public DateTime? DateCreate { get; set; }
  }
}
