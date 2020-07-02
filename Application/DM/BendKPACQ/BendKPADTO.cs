using System;

namespace Application.DM.BendKPACQ
{
  public class BendKPADTO
  {
    public long IdBendKPA { get; set; }
    public long IdBend { get; set; }
    public long IdPeg { get; set; }
    public string NIP { get; set; }
    public string Nama { get; set; }
    public DateTime? DateCreate { get; set; }
  }
}
