using System;

namespace Application.DM.DaftUnitCQ
{
  public class DaftUnitDTO
  {
    public long IdUnit { get; set; }
    public long IdUrus { get; set; }
    public string KdUnit { get; set; }
    public string NmUnit { get; set; }
    public int KdLevel { get; set; }
    public string Type { get; set; }
    public string AkroUnit { get; set; }
    public string Alamat { get; set; }
    public string Telepon { get; set; }
    public int StAktif { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
