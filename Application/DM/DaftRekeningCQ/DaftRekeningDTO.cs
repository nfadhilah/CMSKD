using System;

namespace Application.DM.DaftRekeningCQ
{
  public class DaftRekeningDTO
  {
    public long IdRek { get; set; }
    public string KdPer { get; set; }
    public string NmPer { get; set; }
    public int MtgLevel { get; set; }
    public int KdKhusus { get; set; }
    public long JnsRek { get; set; }
    public long? IdJnsAkun { get; set; }
    public string Type { get; set; }
    public int? StAktif { get; set; }
    public DateTime? DateCreate { get; set; }
  }
}
