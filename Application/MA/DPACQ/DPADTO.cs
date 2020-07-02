using System;

namespace Application.MA.DPACQ
{
  public class DPADTO
  {
    public long IdDPA { get; set; }
    public long IdUnit { get; set; }
    public string KdUnit { get; set; }
    public string NmUnit { get; set; }
    public string NoDPA { get; set; }
    public DateTime? TglDPA { get; set; }
    public string NoSah { get; set; }
    public string Keterangan { get; set; }
    public DateTime? TglValid { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
