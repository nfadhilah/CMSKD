using System;

namespace Application.DM.PPKCQ
{
  public class PPKDTO
  {
    public long IdPPK { get; set; }
    public long IdPeg { get; set; }
    public string NIP { get; set; }
    public string Nama { get; set; }
    public string KdGol { get; set; }
    public string Jabatan { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
